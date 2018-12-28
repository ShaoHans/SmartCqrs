using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartCqrs.API.Extensions;
using SmartCqrs.API.Filters;
using SmartCqrs.Application.Commands;
using SmartCqrs.Application.Dtos;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Auth;
using SmartCqrs.Infrastructure.CommonServices;
using SmartCqrs.Infrastructure.Configuration;
using SmartCqrs.Infrastructure.DapperEx;
using SmartCqrs.Infrastructure.Log;
using SmartCqrs.Query.Services;
using SmartCqrs.Query.Services.Impls;
using SmartCqrs.Repository.Postgresql;
using SmartCqrs.Repository.Postgresql.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace SmartCqrs.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddJsonOptions(options =>
            {
                // API接口返回日期时间类型的数据时使用自定义格式化
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.Configure<AppSettings>(Configuration);

            #region WebApi版本控制及Swagger文档

            services.AddMvcCore().AddApiExplorer().AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(), new HeaderApiVersionReader("api-version"));
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider()
                             .GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                      description.GroupName,
                        new Info()
                        {
                            Title = $"api接口{description.ApiVersion}版本",
                            Version = description.ApiVersion.ToString()
                        });
                }

                List<string> xmlDocs = new List<string>
                {
                    "SmartCqrs.API.xml",
                    "SmartCqrs.Application.xml",
                    "SmartCqrs.Query.xml",
                    "SmartCqrs.Infrastructure.xml",
                    "SmartCqrs.Enumeration.xml"
                };
                foreach (var xmlDoc in xmlDocs)
                {
                    var xmlDocPath = Path.Combine(AppContext.BaseDirectory, xmlDoc);
                    if (File.Exists(xmlDocPath))
                    {
                        options.IncludeXmlComments(xmlDocPath);
                    }
                }

                options.OperationFilter<SwaggerDefaultValues>();
                options.DocumentFilter<SwaggerHideInDocsFilter>();
            });

            #endregion

            // 参考文章：https://www.cnblogs.com/RainingNight/p/jwtbearer-authentication-in-asp-net-core.html
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    var jwtSettings = new JwtSettings();
                    Configuration.GetSection("JwtSettings").Bind(jwtSettings);
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer
                    };
                });

            services.AddDbContext<SmartBlogPostgresqlDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("SmartBlogPostgresql"));
            });
            services.AddScoped(sp => { return new DapperContext(Configuration.GetConnectionString("SmartBlogPostgresql")); });
            services.AddMediatR(typeof(BaseCommandHandler).GetTypeInfo().Assembly);
            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(EfCoreRepositoryBase<>));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(IUserAssetRepository), typeof(UserAssetRepository));
            services.AddSingleton<ILoggerManager, NLoggerManager>();
            services.AddTransient(typeof(ICarQuery), typeof(CarQuery));
            services.AddTransient(typeof(IUserQuery), typeof(UserQuery));
            services.Configure<CommonserviceUrlModel>(Configuration.GetSection("CommonserviceUrl"));
            MapperInitializer.Init();

            services.AddHttpClient();
            services.AddHttpClient<TongHangBrokerCommonServiceClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("IdentityServer:CommonServiceHost").Value);
            });
            services.AddHttpClient<TongHangBrokerAuthServiceClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("IdentityServer:AuthTokenUrl").Value);
            });

            services.AddTransient<IJwtService, JwtService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            ServiceLocator.Instance = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
            }
            else
            {
                app.UseHsts();
            }

            Console.WriteLine(env.EnvironmentName);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            DbContextSeed(app);
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <param name="app"></param>
        private void DbContextSeed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILoggerManager>();
                var context = services.GetService<SmartBlogPostgresqlDbContext>();

                new SmartBlogPostgresqlDbContextSeed().SeedAsync(context, logger).Wait();
            }
        }
    }
}
