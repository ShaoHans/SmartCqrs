using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCqrs.Repository.Postgresql.Migrations
{
    public partial class BlogTableAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "comment_blog_count",
                table: "user_asset",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comment_blog_count",
                table: "user_asset");
        }
    }
}
