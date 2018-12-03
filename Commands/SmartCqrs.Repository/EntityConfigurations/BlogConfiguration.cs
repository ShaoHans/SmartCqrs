using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Repository.EntityConfigurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("blog");
            builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.CoverUrl).HasColumnType("varchar(512)");
            builder.Property(x => x.ViewCount).IsRequired();
            builder.Property(x => x.CommentCount).IsRequired();
            builder.Property(x => x.CollectCount).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CreatedTime).IsRequired();
        }
    }
}
