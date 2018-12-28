using DocsByReflection;
using Microsoft.EntityFrameworkCore;
using SmartCqrs.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace SmartCqrs.Repository.MySql
{
    public class SmartBlogMySqlDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserAsset> UserAssets { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<BlogComment> BlogComments { get; set; }

        public DbSet<BlogCollect> BlogCollects { get; set; }

        public SmartBlogMySqlDbContext(DbContextOptions<SmartBlogMySqlDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var applyGenericMethod = typeof(ModelBuilder).GetMethods().Where(m => m.Name == "ApplyConfiguration" 
            && m.GetParameters().First().ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
            .GetGenericTypeDefinition()).FirstOrDefault();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
                        break;
                    }
                }
            }
        }

        private void DatabaseMetaDataReName(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    // 生成数据库字段注释的两种方式
                    // 1.给类属性加上Description标注，可以当作数据库字段说明
                    //var desAttr = property.PropertyInfo.GetCustomAttribute<DescriptionAttribute>();
                    //if (desAttr != null)
                    //{
                    //    property.Npgsql().Comment = desAttr.Description;
                    //}

                    // 2.通过反射的方式获取类属性的注解
                    // 对于没有注解的类属性，GetXmlFromMember方法的第二个参数传false，不抛出异常
                    XmlElement documentation = DocsService.GetXmlFromMember(property.PropertyInfo, false);
                    if (documentation != null && !string.IsNullOrWhiteSpace(documentation.InnerText))
                    {
                        //property.Npgsql().Comment = documentation.InnerText.Trim();
                    }
                }
            }
        }
    }
}
