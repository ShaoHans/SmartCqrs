using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Repository.EntityConfigurations
{
    public class ViewCarLogConfiguration : IEntityTypeConfiguration<ViewCarLog>
    {
        public void Configure(EntityTypeBuilder<ViewCarLog> builder)
        {
            builder.ToTable("view_car_log");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CarId).IsRequired();
            builder.Property(x => x.Ip).HasColumnType("varchar(64)");
            builder.Property(x => x.CreatedTime).IsRequired();
        }
    }
}
