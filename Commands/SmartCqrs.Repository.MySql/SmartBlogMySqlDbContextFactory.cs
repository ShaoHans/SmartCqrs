using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartCqrs.Repository.MySql
{
    public class SmartBlogMySqlDbContextFactory : IDesignTimeDbContextFactory<SmartBlogMySqlDbContext>
    {
        public SmartBlogMySqlDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SmartBlogMySqlDbContext>();
            builder.UseMySQL("server=localhost;database=SmartBlog;user=root;password=123456");
            return new SmartBlogMySqlDbContext(builder.Options);
        }
    }
}
