using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartCqrs.Repository
{
    public class SmartBlogDbContextFactory : IDesignTimeDbContextFactory<SmartBlogDbContext>
    {
        public SmartBlogDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SmartBlogDbContext>();
            builder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=smart_blog;Pooling=true;");
            return new SmartBlogDbContext(builder.Options);
        }
    }
}
