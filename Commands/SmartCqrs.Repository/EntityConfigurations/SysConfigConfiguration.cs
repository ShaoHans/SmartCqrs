using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Repository.EntityConfigurations
{
    public class SysConfigConfiguration : IEntityTypeConfiguration<SysConfig>
    {
        public void Configure(EntityTypeBuilder<SysConfig> builder)
        {
            builder.ToTable("sys_config");

            builder.Property(x => x.ParamKey).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.ParamValue).HasColumnType("varchar(4000)").IsRequired();
            builder.Property(x => x.Remark).HasColumnType("varchar(500)").IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(1);
            builder.HasIndex(x => x.ParamKey).IsUnique();
        }
    }
}
