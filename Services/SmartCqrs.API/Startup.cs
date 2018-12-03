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
using SmartCqrs.API.ConfigModels;
using SmartCqrs.API.Filters;
using SmartCqrs.Application.Commands;
using SmartCqrs.Application.Dtos;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Auth;
using SmartCqrs.Infrastructure.CommonServices;
using SmartCqrs.Infrastructure.DapperEx;
using SmartCqrs.Infrastructure.Log;
using SmartCqrs.Query.Services;
using SmartCqrs.Query.Services.Impls;
using SmartCqrs.Repository;
using SmartCqrs.Repository.Repositories;
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
                            Title = $"车商邦app接口{description.ApiVersion}版本",
                            Version = description.ApiVersion.ToString()
                        });
                }

                List<string> xmlDocs = new List<string>
                {
                    "CarDealerBang.API.xml",
                    "CarDealerBang.Application.xml",
                    "CarDealerBang.Query.xml",
                    "CarDealerBang.Infrastructure.xml",
                    "CarDealerBang.Enumeration.xml"
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
            services.AddAuthorization(x =>
            {
                #region 自定义验证策略
                JWTHelper.SecurityKey = Configuration.GetSection("IdentityServer:JwtSecurityKey").Value;
                x.AddPolicy("user", policy => policy.Requirements.Add(new CommonAuthorize()));
                #endregion
            })
                .AddAuthentication(x =>
                {
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    //设置需要验证的项目
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTHelper.SecurityKey))
                    };
                });

            services.AddSingleton<IAuthorizationHandler, CommonAuthorizeHandler>();
            services.AddDbContext<SmartBlogDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("SmartBlog"));
            });
            services.AddMediatR(typeof(BaseCommandHandler).GetTypeInfo().Assembly);
            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(EfCoreRepositoryBase<>));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(IUserAssetRepository), typeof(UserAssetRepository));
            services.AddSingleton<ILoggerManager, NLoggerManager>();
            services.AddScoped(sp => { return new DapperContext(Configuration.GetConnectionString("SmartBlog")); });
            services.AddTransient(typeof(ICarQuery), typeof(CarQuery));
            services.AddTransient(typeof(IUserQuery), typeof(UserQuery));
            services.Configure<CommonserviceUrlModel>(Configuration.GetSection("CommonserviceUrl"));
            //服务授权对象
            services.AddSingleton<IServiceAuthorization, ServiceAuthorization>(a =>
            {
                Debug.WriteLine("注入服务授权对象");
                var _ConfigModelIdentityServer = new IdentityServer();
                Configuration.GetSection("IdentityServer").Bind(_ConfigModelIdentityServer);
                return new ServiceAuthorization($"{_ConfigModelIdentityServer.AuthTokenUrl}api/auth/", _ConfigModelIdentityServer.ClientId, _ConfigModelIdentityServer.ClientSecret);
            });
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
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
            app.UseMvc();
            app.UseAuthentication();
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
                var context = services.GetService<SmartBlogDbContext>();

                new SmartBlogDbContextSeed().SeedAsync(context, logger).Wait();
            }
        }
    }
}
