using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Repository.MySql.EntityConfigurations
{
    public class BlogCommentConfiguration : IEntityTypeConfiguration<BlogComment>
    {
        public void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            builder.ToTable("blog_comment");
            builder.Property(x => x.BlogId).IsRequired();
            builder.Property(x => x.Content).IsRequired().HasColumnType("varchar(1000)");
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CreatedTime).IsRequired();
        }
    }
}
