using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Repository.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("purchase_order");

            builder.Property(x => x.OrderNo).HasColumnType("varchar(40)").IsRequired();
            builder.Property(x => x.CarId).IsRequired();
            builder.Property(x => x.Qty).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.OrderDate).IsRequired();
        }
    }
}
