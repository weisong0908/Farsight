using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.Backend.Migrations
{
    public partial class LinkHoldingCategoryWithPortfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PortfolioId",
                table: "HoldingCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HoldingCategories_PortfolioId",
                table: "HoldingCategories",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoldingCategories_Portfolios_PortfolioId",
                table: "HoldingCategories",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoldingCategories_Portfolios_PortfolioId",
                table: "HoldingCategories");

            migrationBuilder.DropIndex(
                name: "IX_HoldingCategories_PortfolioId",
                table: "HoldingCategories");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "HoldingCategories");
        }
    }
}
