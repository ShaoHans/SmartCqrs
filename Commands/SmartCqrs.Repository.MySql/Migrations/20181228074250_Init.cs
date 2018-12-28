using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCqrs.Repository.MySql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CoverUrl = table.Column<string>(type: "varchar(512)", nullable: true),
                    ViewCount = table.Column<int>(nullable: false),
                    CommentCount = table.Column<int>(nullable: false),
                    CollectCount = table.Column<int>(nullable: false),
                    UserId = table.Column<byte[]>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "blog_collect",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BlogId = table.Column<int>(nullable: false),
                    UserId = table.Column<byte[]>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_collect", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "blog_comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BlogId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(type: "varchar(1000)", nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    ReplyCount = table.Column<int>(nullable: false),
                    UserId = table.Column<byte[]>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_comment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<byte[]>(nullable: false),
                    Mobile = table.Column<string>(type: "varchar(16)", nullable: false),
                    Password = table.Column<string>(type: "varchar(40)", nullable: true),
                    NickName = table.Column<string>(type: "varchar(20)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "varchar(512)", nullable: true),
                    ProvinceName = table.Column<string>(type: "varchar(36)", nullable: true),
                    CityName = table.Column<string>(type: "varchar(36)", nullable: true),
                    RegisterTime = table.Column<DateTime>(nullable: false),
                    RegisterChannel = table.Column<int>(nullable: false, defaultValue: 0),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_asset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<byte[]>(nullable: false),
                    TotalPoint = table.Column<int>(nullable: false),
                    PublishBlogCount = table.Column<int>(nullable: false),
                    CollectBlogCount = table.Column<int>(nullable: false),
                    CommentBlogCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_asset", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_Mobile",
                table: "user",
                column: "Mobile",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog");

            migrationBuilder.DropTable(
                name: "blog_collect");

            migrationBuilder.DropTable(
                name: "blog_comment");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "user_asset");
        }
    }
}
