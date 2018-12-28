using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Repository.MySql.EntityConfigurations
{
    public class BlogCollectConfiguration : IEntityTypeConfiguration<BlogCollect>
    {
        public void Configure(EntityTypeBuilder<BlogCollect> builder)
        {
            builder.ToTable("blog_collect");
            builder.Property(x => x.BlogId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CreatedTime).IsRequired();
        }
    }
}
