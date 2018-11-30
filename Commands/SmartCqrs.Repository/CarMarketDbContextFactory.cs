using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartCqrs.Repository
{
    public class CarMarketDbContextFactory : IDesignTimeDbContextFactory<CarMarketDbContext>
    {
        public CarMarketDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CarMarketDbContext>();
            builder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=car_market;Pooling=true;");
            return new CarMarketDbContext(builder.Options);
        }
    }
}
