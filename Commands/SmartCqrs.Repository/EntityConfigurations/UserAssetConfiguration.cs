using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Repository.EntityConfigurations
{
    public class UserAssetConfiguration : IEntityTypeConfiguration<UserAsset>
    {
        public void Configure(EntityTypeBuilder<UserAsset> builder)
        {
            builder.ToTable("user_asset");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.SellingCarCount).IsRequired();
            builder.Property(x => x.CollectCarCount).IsRequired();
            builder.Property(x => x.OrderCount).IsRequired();
            builder.Property(x => x.UpdatedTime).IsRequired();
        }
    }
}
