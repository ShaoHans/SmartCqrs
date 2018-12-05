using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SmartCqrs.Repository.Migrations
{
    public partial class ColumnAddComment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "user_info",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", "用户id")
                .OldAnnotation("Npgsql:Comment", @"
            用户id
            ");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "user_info",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0)
                .Annotation("Npgsql:Comment", "用户状态（0：有效，9：已删除）")
                .OldAnnotation("Npgsql:Comment", @"
            用户状态（0：有效，9：已删除）
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "register_time",
                table: "user_info",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("Npgsql:Comment", "注册时间")
                .OldAnnotation("Npgsql:Comment", @"
            注册时间
            ");

            migrationBuilder.AlterColumn<int>(
                name: "register_channel",
                table: "user_info",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0)
                .Annotation("Npgsql:Comment", "注册渠道（0：App注册，1：后台手动添加，2：Web注册）")
                .OldAnnotation("Npgsql:Comment", @"
            注册渠道（0：App注册，1：后台手动添加，2：Web注册）
            ");

            migrationBuilder.AlterColumn<string>(
                name: "province_name",
                table: "user_info",
                type: "varchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", "省份名称")
                .OldAnnotation("Npgsql:Comment", @"
            省份名称
            ");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user_info",
                type: "varchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", "密码")
                .OldAnnotation("Npgsql:Comment", @"
            密码
            ");

            migrationBuilder.AlterColumn<string>(
                name: "nick_name",
                table: "user_info",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", "昵称")
                .OldAnnotation("Npgsql:Comment", @"
            昵称
            ");

            migrationBuilder.AlterColumn<string>(
                name: "mobile",
                table: "user_info",
                type: "varchar(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)")
                .Annotation("Npgsql:Comment", "手机号码")
                .OldAnnotation("Npgsql:Comment", @"
            手机号码
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_login_time",
                table: "user_info",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true)
                .Annotation("Npgsql:Comment", "最近一次登录时间")
                .OldAnnotation("Npgsql:Comment", @"
            最近一次登录时间
            ");

            migrationBuilder.AlterColumn<string>(
                name: "city_name",
                table: "user_info",
                type: "varchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", "所在城市名称")
                .OldAnnotation("Npgsql:Comment", @"
            所在城市名称
            ");

            migrationBuilder.AlterColumn<string>(
                name: "avatar_url",
                table: "user_info",
                type: "varchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", "头像Url")
                .OldAnnotation("Npgsql:Comment", @"
            头像Url
            ");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_info",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "主键标识")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", @"
            主键标识
            ")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", "用户uuid")
                .OldAnnotation("Npgsql:Comment", @"
            用户uuid
            ");

            migrationBuilder.AlterColumn<int>(
                name: "total_point",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "总积分")
                .OldAnnotation("Npgsql:Comment", @"
            总积分
            ");

            migrationBuilder.AlterColumn<int>(
                name: "publish_blog_count",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "发布的博客文章数量")
                .OldAnnotation("Npgsql:Comment", @"
            发布的博客文章数量
            ");

            migrationBuilder.AlterColumn<int>(
                name: "comment_blog_count",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "评论的博客文章数量")
                .OldAnnotation("Npgsql:Comment", @"
            评论的博客文章数量
            ");

            migrationBuilder.AlterColumn<int>(
                name: "collect_blog_count",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "收藏的博客文章数量")
                .OldAnnotation("Npgsql:Comment", @"
            收藏的博客文章数量
            ");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "主键标识")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", @"
            主键标识
            ")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", "评论人id")
                .OldAnnotation("Npgsql:Comment", @"
            评论人id
            ");

            migrationBuilder.AlterColumn<int>(
                name: "reply_count",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "回复数量")
                .OldAnnotation("Npgsql:Comment", @"
            回复数量
            ");

            migrationBuilder.AlterColumn<int>(
                name: "like_count",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "点赞数量")
                .OldAnnotation("Npgsql:Comment", @"
            点赞数量
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_time",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("Npgsql:Comment", "评论时间")
                .OldAnnotation("Npgsql:Comment", @"
            评论时间
            ");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "blog_comment",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)")
                .Annotation("Npgsql:Comment", "评论内容")
                .OldAnnotation("Npgsql:Comment", @"
            评论内容
            ");

            migrationBuilder.AlterColumn<int>(
                name: "blog_id",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "博客Id")
                .OldAnnotation("Npgsql:Comment", @"
            博客Id
            ");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "主键标识")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", @"
            主键标识
            ")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "blog_collect",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", "收藏用户Id")
                .OldAnnotation("Npgsql:Comment", @"
            收藏用户Id
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_time",
                table: "blog_collect",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("Npgsql:Comment", "收藏时间")
                .OldAnnotation("Npgsql:Comment", @"
            收藏时间
            ");

            migrationBuilder.AlterColumn<int>(
                name: "blog_id",
                table: "blog_collect",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "博客Id")
                .OldAnnotation("Npgsql:Comment", @"
            博客Id
            ");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "blog_collect",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "主键标识")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", @"
            主键标识
            ")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "view_count",
                table: "blog",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "浏览次数")
                .OldAnnotation("Npgsql:Comment", @"
            浏览次数
            ");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "blog",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", "作者id")
                .OldAnnotation("Npgsql:Comment", @"
            作者id
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_time",
                table: "blog",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true)
                .Annotation("Npgsql:Comment", "更新时间")
                .OldAnnotation("Npgsql:Comment", @"
            更新时间
            ");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "blog",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .Annotation("Npgsql:Comment", "标题")
                .OldAnnotation("Npgsql:Comment", @"
            标题
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_time",
                table: "blog",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("Npgsql:Comment", "发表时间")
                .OldAnnotation("Npgsql:Comment", @"
            发表时间
            ");

            migrationBuilder.AlterColumn<string>(
                name: "cover_url",
                table: "blog",
                type: "varchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", "封面图")
                .OldAnnotation("Npgsql:Comment", @"
            封面图
            ");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "blog",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("Npgsql:Comment", "内容")
                .OldAnnotation("Npgsql:Comment", @"
            内容
            ");

            migrationBuilder.AlterColumn<int>(
                name: "comment_count",
                table: "blog",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "评论数量")
                .OldAnnotation("Npgsql:Comment", @"
            评论数量
            ");

            migrationBuilder.AlterColumn<int>(
                name: "collect_count",
                table: "blog",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "收藏数量")
                .OldAnnotation("Npgsql:Comment", @"
            收藏数量
            ");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "blog",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", "主键标识")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", @"
            主键标识
            ")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "user_info",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", @"
            用户id
            ")
                .OldAnnotation("Npgsql:Comment", "用户id");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "user_info",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0)
                .Annotation("Npgsql:Comment", @"
            用户状态（0：有效，9：已删除）
            ")
                .OldAnnotation("Npgsql:Comment", "用户状态（0：有效，9：已删除）");

            migrationBuilder.AlterColumn<DateTime>(
                name: "register_time",
                table: "user_info",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("Npgsql:Comment", @"
            注册时间
            ")
                .OldAnnotation("Npgsql:Comment", "注册时间");

            migrationBuilder.AlterColumn<int>(
                name: "register_channel",
                table: "user_info",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0)
                .Annotation("Npgsql:Comment", @"
            注册渠道（0：App注册，1：后台手动添加，2：Web注册）
            ")
                .OldAnnotation("Npgsql:Comment", "注册渠道（0：App注册，1：后台手动添加，2：Web注册）");

            migrationBuilder.AlterColumn<string>(
                name: "province_name",
                table: "user_info",
                type: "varchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", @"
            省份名称
            ")
                .OldAnnotation("Npgsql:Comment", "省份名称");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user_info",
                type: "varchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", @"
            密码
            ")
                .OldAnnotation("Npgsql:Comment", "密码");

            migrationBuilder.AlterColumn<string>(
                name: "nick_name",
                table: "user_info",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", @"
            昵称
            ")
                .OldAnnotation("Npgsql:Comment", "昵称");

            migrationBuilder.AlterColumn<string>(
                name: "mobile",
                table: "user_info",
                type: "varchar(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)")
                .Annotation("Npgsql:Comment", @"
            手机号码
            ")
                .OldAnnotation("Npgsql:Comment", "手机号码");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_login_time",
                table: "user_info",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true)
                .Annotation("Npgsql:Comment", @"
            最近一次登录时间
            ")
                .OldAnnotation("Npgsql:Comment", "最近一次登录时间");

            migrationBuilder.AlterColumn<string>(
                name: "city_name",
                table: "user_info",
                type: "varchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", @"
            所在城市名称
            ")
                .OldAnnotation("Npgsql:Comment", "所在城市名称");

            migrationBuilder.AlterColumn<string>(
                name: "avatar_url",
                table: "user_info",
                type: "varchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", @"
            头像Url
            ")
                .OldAnnotation("Npgsql:Comment", "头像Url");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_info",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            主键标识
            ")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", "主键标识")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", @"
            用户uuid
            ")
                .OldAnnotation("Npgsql:Comment", "用户uuid");

            migrationBuilder.AlterColumn<int>(
                name: "total_point",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            总积分
            ")
                .OldAnnotation("Npgsql:Comment", "总积分");

            migrationBuilder.AlterColumn<int>(
                name: "publish_blog_count",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            发布的博客文章数量
            ")
                .OldAnnotation("Npgsql:Comment", "发布的博客文章数量");

            migrationBuilder.AlterColumn<int>(
                name: "comment_blog_count",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            评论的博客文章数量
            ")
                .OldAnnotation("Npgsql:Comment", "评论的博客文章数量");

            migrationBuilder.AlterColumn<int>(
                name: "collect_blog_count",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            收藏的博客文章数量
            ")
                .OldAnnotation("Npgsql:Comment", "收藏的博客文章数量");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_asset",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            主键标识
            ")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", "主键标识")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", @"
            评论人id
            ")
                .OldAnnotation("Npgsql:Comment", "评论人id");

            migrationBuilder.AlterColumn<int>(
                name: "reply_count",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            回复数量
            ")
                .OldAnnotation("Npgsql:Comment", "回复数量");

            migrationBuilder.AlterColumn<int>(
                name: "like_count",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            点赞数量
            ")
                .OldAnnotation("Npgsql:Comment", "点赞数量");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_time",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("Npgsql:Comment", @"
            评论时间
            ")
                .OldAnnotation("Npgsql:Comment", "评论时间");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "blog_comment",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)")
                .Annotation("Npgsql:Comment", @"
            评论内容
            ")
                .OldAnnotation("Npgsql:Comment", "评论内容");

            migrationBuilder.AlterColumn<int>(
                name: "blog_id",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            博客Id
            ")
                .OldAnnotation("Npgsql:Comment", "博客Id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "blog_comment",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            主键标识
            ")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", "主键标识")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "blog_collect",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", @"
            收藏用户Id
            ")
                .OldAnnotation("Npgsql:Comment", "收藏用户Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_time",
                table: "blog_collect",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("Npgsql:Comment", @"
            收藏时间
            ")
                .OldAnnotation("Npgsql:Comment", "收藏时间");

            migrationBuilder.AlterColumn<int>(
                name: "blog_id",
                table: "blog_collect",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            博客Id
            ")
                .OldAnnotation("Npgsql:Comment", "博客Id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "blog_collect",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            主键标识
            ")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", "主键标识")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "view_count",
                table: "blog",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            浏览次数
            ")
                .OldAnnotation("Npgsql:Comment", "浏览次数");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "blog",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", @"
            作者id
            ")
                .OldAnnotation("Npgsql:Comment", "作者id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_time",
                table: "blog",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true)
                .Annotation("Npgsql:Comment", @"
            更新时间
            ")
                .OldAnnotation("Npgsql:Comment", "更新时间");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "blog",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .Annotation("Npgsql:Comment", @"
            标题
            ")
                .OldAnnotation("Npgsql:Comment", "标题");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_time",
                table: "blog",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("Npgsql:Comment", @"
            发表时间
            ")
                .OldAnnotation("Npgsql:Comment", "发表时间");

            migrationBuilder.AlterColumn<string>(
                name: "cover_url",
                table: "blog",
                type: "varchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", @"
            封面图
            ")
                .OldAnnotation("Npgsql:Comment", "封面图");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "blog",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("Npgsql:Comment", @"
            内容
            ")
                .OldAnnotation("Npgsql:Comment", "内容");

            migrationBuilder.AlterColumn<int>(
                name: "comment_count",
                table: "blog",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            评论数量
            ")
                .OldAnnotation("Npgsql:Comment", "评论数量");

            migrationBuilder.AlterColumn<int>(
                name: "collect_count",
                table: "blog",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            收藏数量
            ")
                .OldAnnotation("Npgsql:Comment", "收藏数量");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "blog",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:Comment", @"
            主键标识
            ")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:Comment", "主键标识")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);
        }
    }
}
