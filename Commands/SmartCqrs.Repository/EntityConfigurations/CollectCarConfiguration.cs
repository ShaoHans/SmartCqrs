using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Repository.EntityConfigurations
{
    public class CollectCarConfiguration : IEntityTypeConfiguration<CollectCar>
    {
        public void Configure(EntityTypeBuilder<CollectCar> builder)
        {
            builder.ToTable("collect_car");
            builder.Property(x => x.CarId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CollectedTime).IsRequired();
        }
    }
}
