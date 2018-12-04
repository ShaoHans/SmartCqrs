using System;
using System.Collections.Generic;
using System.Text;
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

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.TotalPoint).IsRequired();
            builder.Property(x => x.PublishBlogCount).IsRequired();
            builder.Property(x => x.CollectBlogCount).IsRequired();
            builder.Property(x => x.CommentBlogCount).IsRequired();
        }
    }
}
