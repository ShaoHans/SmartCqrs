using Microsoft.EntityFrameworkCore;
using SmartCqrs.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SmartCqrs.Repository
{
    public class SmartBlogDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserAsset> UserAssets { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<BlogComment> BlogComments { get; set; }

        public DbSet<BlogCollect> BlogCollects { get; set; }

        public SmartBlogDbContext(DbContextOptions<SmartBlogDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            DatabaseMetaDataReName(modelBuilder);

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

        /// <summary>
        /// 如果是使用PostgreSql，则使用snake_case命名法
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void DatabaseMetaDataReName(ModelBuilder modelBuilder)
        {
            // 参考文章
            // https://andrewlock.net/customising-asp-net-core-identity-ef-core-naming-conventions-for-postgresql/

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                //Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    // 当类属性加上Description标注，可以当作数据库字段说明
                    var desAttr = property.PropertyInfo.GetCustomAttribute<DescriptionAttribute>();
                    if (desAttr != null)
                    {
                        property.Npgsql().Comment = desAttr.Description;
                    }
                    property.Relational().ColumnName = property.Name.ToSnakeCase();
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }
        }
    }
}
