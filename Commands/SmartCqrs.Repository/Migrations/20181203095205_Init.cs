using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SmartCqrs.Repository.Postgresql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    title = table.Column<string>(type: "varchar(100)", nullable: false),
                    content = table.Column<string>(nullable: false),
                    cover_url = table.Column<string>(type: "varchar(512)", nullable: true),
                    view_count = table.Column<int>(nullable: false),
                    comment_count = table.Column<int>(nullable: false),
                    collect_count = table.Column<int>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    created_time = table.Column<DateTime>(nullable: false),
                    updated_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blog_collect",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    blog_id = table.Column<int>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    created_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blog_collects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blog_comment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    blog_id = table.Column<int>(nullable: false),
                    content = table.Column<string>(type: "varchar(1000)", nullable: false),
                    like_count = table.Column<int>(nullable: false),
                    reply_count = table.Column<int>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    created_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blog_comments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_asset",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<Guid>(nullable: false),
                    total_point = table.Column<int>(nullable: false),
                    publish_blog_count = table.Column<int>(nullable: false),
                    collect_blog_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_assets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_info",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<Guid>(nullable: false),
                    mobile = table.Column<string>(type: "varchar(16)", nullable: false),
                    password = table.Column<string>(type: "varchar(40)", nullable: true),
                    nick_name = table.Column<string>(type: "varchar(20)", nullable: true),
                    avatar_url = table.Column<string>(type: "varchar(512)", nullable: true),
                    province_name = table.Column<string>(type: "varchar(36)", nullable: true),
                    city_name = table.Column<string>(type: "varchar(36)", nullable: true),
                    register_time = table.Column<DateTime>(nullable: false),
                    register_channel = table.Column<int>(nullable: false, defaultValue: 0),
                    last_login_time = table.Column<DateTime>(nullable: true),
                    status = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_info_mobile",
                table: "user_info",
                column: "mobile",
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
                name: "user_asset");

            migrationBuilder.DropTable(
                name: "user_info");
        }
    }
}
