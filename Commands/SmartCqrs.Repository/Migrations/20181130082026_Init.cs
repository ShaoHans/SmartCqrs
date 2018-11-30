using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SmartCqrs.Repository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    brand_name = table.Column<string>(type: "varchar(40)", nullable: false)
                        .Annotation("Npgsql:Comment", "车品牌名称"),
                    series_name = table.Column<string>(type: "varchar(40)", nullable: false)
                        .Annotation("Npgsql:Comment", "车系名称"),
                    style_name = table.Column<string>(type: "varchar(40)", nullable: true)
                        .Annotation("Npgsql:Comment", "车款式名称"),
                    model_name = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("Npgsql:Comment", "车型名称"),
                    sales_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                        .Annotation("Npgsql:Comment", "售价"),
                    tag = table.Column<string>(type: "varchar(18)", nullable: true)
                        .Annotation("Npgsql:Comment", "标签"),
                    description = table.Column<string>(type: "varchar(1000)", nullable: false)
                        .Annotation("Npgsql:Comment", "描述"),
                    image = table.Column<string>(type: "jsonb", nullable: false)
                        .Annotation("Npgsql:Comment", "车辆图片，以jsonb格式存储"),
                    stock_qty = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Npgsql:Comment", "库存数量"),
                    sales_qty = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Npgsql:Comment", "已售数量"),
                    view_count = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Npgsql:Comment", "浏览数量"),
                    collect_count = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Npgsql:Comment", "收藏数量"),
                    order_count = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Npgsql:Comment", "产生的订单数量"),
                    status = table.Column<int>(nullable: false, defaultValue: 1)
                        .Annotation("Npgsql:Comment", "状态（1：售卖中，2：已下架，9：已删除）"),
                    user_id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "发布车辆用户Id"),
                    published_time = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "发布时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cars", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "collect_car",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    car_id = table.Column<int>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    collected_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collect_cars", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    order_no = table.Column<string>(type: "varchar(40)", nullable: false),
                    car_id = table.Column<int>(nullable: false),
                    qty = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    order_date = table.Column<DateTime>(nullable: false),
                    updated_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_config",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    param_key = table.Column<string>(type: "varchar(100)", nullable: false),
                    param_value = table.Column<string>(type: "varchar(4000)", nullable: false),
                    status = table.Column<int>(nullable: false, defaultValue: 1),
                    remark = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_configs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_asset",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<Guid>(nullable: false),
                    selling_car_count = table.Column<int>(nullable: false),
                    collect_car_count = table.Column<int>(nullable: false),
                    order_count = table.Column<int>(nullable: false),
                    updated_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_assets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "userinfo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<Guid>(nullable: false),
                    mobile = table.Column<string>(type: "varchar(16)", nullable: false),
                    password = table.Column<string>(type: "varchar(40)", nullable: true),
                    nick_name = table.Column<string>(type: "varchar(20)", nullable: true),
                    real_name = table.Column<string>(type: "varchar(20)", nullable: true),
                    avatar = table.Column<string>(type: "varchar(512)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "view_car_log",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    car_id = table.Column<int>(nullable: false),
                    ip = table.Column<string>(type: "varchar(64)", nullable: true),
                    user_id = table.Column<Guid>(nullable: true),
                    created_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_view_car_logs", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sys_config_param_key",
                table: "sys_config",
                column: "param_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userinfo_mobile",
                table: "userinfo",
                column: "mobile",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "collect_car");

            migrationBuilder.DropTable(
                name: "purchase_order");

            migrationBuilder.DropTable(
                name: "sys_config");

            migrationBuilder.DropTable(
                name: "user_asset");

            migrationBuilder.DropTable(
                name: "userinfo");

            migrationBuilder.DropTable(
                name: "view_car_log");
        }
    }
}
