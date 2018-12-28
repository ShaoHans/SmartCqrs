﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartCqrs.Enumeration;
using SmartCqrs.Repository.Postgresql;

namespace SmartCqrs.Repository.Postgresql.Migrations
{
    [DbContext(typeof(SmartBlogPostgresqlDbContext))]
    partial class SmartBlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SmartCqrs.Domain.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:Comment", "主键标识");

                    b.Property<int>("CollectCount")
                        .HasColumnName("collect_count")
                        .HasAnnotation("Npgsql:Comment", "收藏数量");

                    b.Property<int>("CommentCount")
                        .HasColumnName("comment_count")
                        .HasAnnotation("Npgsql:Comment", "评论数量");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasAnnotation("Npgsql:Comment", "内容");

                    b.Property<string>("CoverUrl")
                        .HasColumnName("cover_url")
                        .HasColumnType("varchar(512)")
                        .HasAnnotation("Npgsql:Comment", "封面图");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time")
                        .HasAnnotation("Npgsql:Comment", "发表时间");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Npgsql:Comment", "标题");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnName("updated_time")
                        .HasAnnotation("Npgsql:Comment", "更新时间");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasAnnotation("Npgsql:Comment", "作者id");

                    b.Property<int>("ViewCount")
                        .HasColumnName("view_count")
                        .HasAnnotation("Npgsql:Comment", "浏览次数");

                    b.HasKey("Id")
                        .HasName("pk_blogs");

                    b.ToTable("blog");
                });

            modelBuilder.Entity("SmartCqrs.Domain.Models.BlogCollect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:Comment", "主键标识");

                    b.Property<int>("BlogId")
                        .HasColumnName("blog_id")
                        .HasAnnotation("Npgsql:Comment", "博客Id");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time")
                        .HasAnnotation("Npgsql:Comment", "收藏时间");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasAnnotation("Npgsql:Comment", "收藏用户Id");

                    b.HasKey("Id")
                        .HasName("pk_blog_collects");

                    b.ToTable("blog_collect");
                });

            modelBuilder.Entity("SmartCqrs.Domain.Models.BlogComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:Comment", "主键标识");

                    b.Property<int>("BlogId")
                        .HasColumnName("blog_id")
                        .HasAnnotation("Npgsql:Comment", "博客Id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("varchar(1000)")
                        .HasAnnotation("Npgsql:Comment", "评论内容");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time")
                        .HasAnnotation("Npgsql:Comment", "评论时间");

                    b.Property<int>("LikeCount")
                        .HasColumnName("like_count")
                        .HasAnnotation("Npgsql:Comment", "点赞数量");

                    b.Property<int>("ReplyCount")
                        .HasColumnName("reply_count")
                        .HasAnnotation("Npgsql:Comment", "回复数量");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasAnnotation("Npgsql:Comment", "评论人id");

                    b.HasKey("Id")
                        .HasName("pk_blog_comments");

                    b.ToTable("blog_comment");
                });

            modelBuilder.Entity("SmartCqrs.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:Comment", "主键标识");

                    b.Property<string>("AvatarUrl")
                        .HasColumnName("avatar_url")
                        .HasColumnType("varchar(512)")
                        .HasAnnotation("Npgsql:Comment", "头像Url");

                    b.Property<string>("CityName")
                        .HasColumnName("city_name")
                        .HasColumnType("varchar(36)")
                        .HasAnnotation("Npgsql:Comment", "所在城市名称");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnName("last_login_time")
                        .HasAnnotation("Npgsql:Comment", "最近一次登录时间");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnName("mobile")
                        .HasColumnType("varchar(16)")
                        .HasAnnotation("Npgsql:Comment", "手机号码");

                    b.Property<string>("NickName")
                        .HasColumnName("nick_name")
                        .HasColumnType("varchar(20)")
                        .HasAnnotation("Npgsql:Comment", "昵称");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("varchar(40)")
                        .HasAnnotation("Npgsql:Comment", "密码");

                    b.Property<string>("ProvinceName")
                        .HasColumnName("province_name")
                        .HasColumnType("varchar(36)")
                        .HasAnnotation("Npgsql:Comment", "省份名称");

                    b.Property<int>("RegisterChannel")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("register_channel")
                        .HasDefaultValue(0)
                        .HasAnnotation("Npgsql:Comment", "注册渠道（0：App注册，1：后台手动添加，2：Web注册）");

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnName("register_time")
                        .HasAnnotation("Npgsql:Comment", "注册时间");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("status")
                        .HasDefaultValue(0)
                        .HasAnnotation("Npgsql:Comment", "用户状态（0：有效，9：已删除）");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasAnnotation("Npgsql:Comment", "用户id");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Mobile")
                        .IsUnique();

                    b.ToTable("user_info");
                });

            modelBuilder.Entity("SmartCqrs.Domain.Models.UserAsset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:Comment", "主键标识");

                    b.Property<int>("CollectBlogCount")
                        .HasColumnName("collect_blog_count")
                        .HasAnnotation("Npgsql:Comment", "收藏的博客文章数量");

                    b.Property<int>("CommentBlogCount")
                        .HasColumnName("comment_blog_count")
                        .HasAnnotation("Npgsql:Comment", "评论的博客文章数量");

                    b.Property<int>("PublishBlogCount")
                        .HasColumnName("publish_blog_count")
                        .HasAnnotation("Npgsql:Comment", "发布的博客文章数量");

                    b.Property<int>("TotalPoint")
                        .HasColumnName("total_point")
                        .HasAnnotation("Npgsql:Comment", "总积分");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasAnnotation("Npgsql:Comment", "用户uuid");

                    b.HasKey("Id")
                        .HasName("pk_user_assets");

                    b.ToTable("user_asset");
                });
#pragma warning restore 612, 618
        }
    }
}
