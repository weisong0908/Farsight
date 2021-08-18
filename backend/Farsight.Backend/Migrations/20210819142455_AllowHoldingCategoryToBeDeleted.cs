using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.Backend.Migrations
{
    public partial class AllowHoldingCategoryToBeDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_HoldingCategories_HoldingCategoryId",
                table: "Holdings");

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_HoldingCategories_HoldingCategoryId",
                table: "Holdings",
                column: "HoldingCategoryId",
                principalTable: "HoldingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_HoldingCategories_HoldingCategoryId",
                table: "Holdings");

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_HoldingCategories_HoldingCategoryId",
                table: "Holdings",
                column: "HoldingCategoryId",
                principalTable: "HoldingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
