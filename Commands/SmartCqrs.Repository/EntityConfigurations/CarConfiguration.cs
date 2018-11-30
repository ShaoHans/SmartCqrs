using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;
using SmartCqrs.Enumeration;

namespace SmartCqrs.Repository.EntityConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("car");
            builder.Property(x => x.BrandName).HasColumnType("varchar(40)").IsRequired();
            builder.Property(x => x.SeriesName).HasColumnType("varchar(40)").IsRequired();
            builder.Property(x => x.StyleName).HasColumnType("varchar(40)");
            builder.Property(x => x.ModelName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.SalesPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Tag).HasColumnType("varchar(18)");
            builder.Property(x => x.Description).HasColumnType("varchar(1000)").IsRequired();
            builder.Property(x => x.Image).HasColumnType("jsonb").IsRequired();
            builder.Property(x => x.StockQty).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.SalesQty).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.CollectCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.OrderCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(CarStatus.Selling);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.PublishedTime).IsRequired();
        }
    }
}
