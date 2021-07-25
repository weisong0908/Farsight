using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.Backend.Migrations
{
    public partial class AddDateToTrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Trades",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: new Guid("5df7c00b-3cfd-48e7-a674-6aee0f120313"),
                column: "Date",
                value: new DateTime(2021, 7, 25, 12, 39, 51, 411, DateTimeKind.Local).AddTicks(2330));

            migrationBuilder.UpdateData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: new Guid("850941ee-b257-4439-b8b6-95a0edc55200"),
                column: "Date",
                value: new DateTime(2021, 7, 25, 4, 39, 51, 416, DateTimeKind.Utc).AddTicks(2200));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Trades");
        }
    }
}
