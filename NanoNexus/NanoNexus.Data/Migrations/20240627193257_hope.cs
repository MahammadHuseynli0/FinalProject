using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NanoNexus.Data.Migrations
{
    public partial class hope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(7995),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(9818));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Sliders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(7132),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(9439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Settings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(6830),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(8911));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(6504),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(8468));

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Colors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(6006),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(7572));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(5708),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(7091));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(5334),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(6351));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(9818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(7995));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Sliders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(9439),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(7132));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Settings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(8911),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(6830));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(8468),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(6504));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Colors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(7572),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(6006));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(7091),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(5708));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 26, 2, 36, 22, 725, DateTimeKind.Utc).AddTicks(6351),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 27, 23, 32, 56, 706, DateTimeKind.Utc).AddTicks(5334));
        }
    }
}
