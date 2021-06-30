using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.Backend.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Portfolios",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("108e7c96-ea21-44f2-9cbe-db4237c2d1dd"), "Portfolio 1" },
                    { new Guid("41881c20-df28-43df-8e1c-e42748181ea3"), "Portfolio 2" }
                });

            migrationBuilder.InsertData(
                table: "Holdings",
                columns: new[] { "Id", "PortfolioId", "Ticker" },
                values: new object[,]
                {
                    { new Guid("6865a7fa-6866-4516-9002-53cc8386991e"), new Guid("108e7c96-ea21-44f2-9cbe-db4237c2d1dd"), "AAPL" },
                    { new Guid("f5f1e765-a3bb-44bb-89b9-52ab8eab9db4"), new Guid("108e7c96-ea21-44f2-9cbe-db4237c2d1dd"), "MSFT" }
                });

            migrationBuilder.InsertData(
                table: "Trades",
                columns: new[] { "Id", "Fees", "HoldingId", "Quantity", "Remarks", "UnitPrice", "tradeType" },
                values: new object[,]
                {
                    { new Guid("5df7c00b-3cfd-48e7-a674-6aee0f120313"), 0m, new Guid("6865a7fa-6866-4516-9002-53cc8386991e"), 10, null, 100m, 0 },
                    { new Guid("850941ee-b257-4439-b8b6-95a0edc55200"), 1m, new Guid("f5f1e765-a3bb-44bb-89b9-52ab8eab9db4"), 6, null, 250m, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Portfolios",
                keyColumn: "Id",
                keyValue: new Guid("41881c20-df28-43df-8e1c-e42748181ea3"));

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: new Guid("5df7c00b-3cfd-48e7-a674-6aee0f120313"));

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: new Guid("850941ee-b257-4439-b8b6-95a0edc55200"));

            migrationBuilder.DeleteData(
                table: "Holdings",
                keyColumn: "Id",
                keyValue: new Guid("6865a7fa-6866-4516-9002-53cc8386991e"));

            migrationBuilder.DeleteData(
                table: "Holdings",
                keyColumn: "Id",
                keyValue: new Guid("f5f1e765-a3bb-44bb-89b9-52ab8eab9db4"));

            migrationBuilder.DeleteData(
                table: "Portfolios",
                keyColumn: "Id",
                keyValue: new Guid("108e7c96-ea21-44f2-9cbe-db4237c2d1dd"));
        }
    }
}
