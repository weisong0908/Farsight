using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.IdentityService.Migrations
{
    public partial class AddStatusMessageToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "AspNetUsers",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "AspNetUsers");
        }
    }
}
