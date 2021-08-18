using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.Backend.Migrations
{
    public partial class AddHoldingCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HoldingCategoryId",
                table: "Holdings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HoldingCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldingCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_HoldingCategoryId",
                table: "Holdings",
                column: "HoldingCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_HoldingCategories_HoldingCategoryId",
                table: "Holdings",
                column: "HoldingCategoryId",
                principalTable: "HoldingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_HoldingCategories_HoldingCategoryId",
                table: "Holdings");

            migrationBuilder.DropTable(
                name: "HoldingCategories");

            migrationBuilder.DropIndex(
                name: "IX_Holdings_HoldingCategoryId",
                table: "Holdings");

            migrationBuilder.DropColumn(
                name: "HoldingCategoryId",
                table: "Holdings");
        }
    }
}
