using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartCqrs.Repository.Postgresql
{
    public class SmartBlogPostgresqlDbContextFactory : IDesignTimeDbContextFactory<SmartBlogPostgresqlDbContext>
    {
        public SmartBlogPostgresqlDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SmartBlogPostgresqlDbContext>();
            builder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=smart_blog;Pooling=true;");
            return new SmartBlogPostgresqlDbContext(builder.Options);
        }
    }
}
