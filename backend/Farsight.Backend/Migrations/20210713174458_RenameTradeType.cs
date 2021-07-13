using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.Backend.Migrations
{
    public partial class RenameTradeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tradeType",
                table: "Trades",
                newName: "TradeType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TradeType",
                table: "Trades",
                newName: "tradeType");
        }
    }
}
