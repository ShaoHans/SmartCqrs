using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCqrs.Repository.Migrations
{
    public partial class ColumnOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "car",
                type: "varchar(18)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(18)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", "标签（只有一个）")
                .OldAnnotation("Npgsql:Comment", "标签");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "car",
                type: "varchar(18)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(18)",
                oldNullable: true)
                .Annotation("Npgsql:Comment", "标签")
                .OldAnnotation("Npgsql:Comment", "标签（只有一个）");
        }
    }
}
