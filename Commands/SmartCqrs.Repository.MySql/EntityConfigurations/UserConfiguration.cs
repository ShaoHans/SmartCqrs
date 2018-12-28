using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;
using SmartCqrs.Enumeration;

namespace SmartCqrs.Repository.MySql.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Mobile).HasColumnType("varchar(16)").IsRequired();
            builder.Property(x => x.Password).HasColumnType("varchar(40)");
            builder.Property(x => x.NickName).HasColumnType("varchar(20)");
            builder.Property(x => x.AvatarUrl).HasColumnType("varchar(512)");
            builder.Property(x => x.ProvinceName).HasColumnType("varchar(36)");
            builder.Property(x => x.CityName).HasColumnType("varchar(36)");
            builder.Property(x => x.RegisterTime).IsRequired();
            builder.Property(x => x.RegisterChannel).IsRequired().HasDefaultValue(RegisterChannel.App);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(UserStatus.Actived);

            // 手机号码添加唯一约束
            builder.HasIndex(x => x.Mobile).IsUnique();
        }
    }
}
