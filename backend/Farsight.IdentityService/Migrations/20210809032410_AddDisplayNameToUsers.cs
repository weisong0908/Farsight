using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.IdentityService.Migrations
{
    public partial class AddDisplayNameToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "1f6a751a-42fb-4c6c-b530-7049fcff47e1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af4b8b92-8d79-492e-891a-73c8fa3e5313", "AQAAAAEAACcQAAAAEI/yL4Z2kcvUyTCo3bAvbBdTMqnmQDP4HyGc354ApxHNuzNk8XQ3G0CFWRmkl5Bu+Q==", "cd46fbbf-c734-4962-9700-6b5591f0ee37" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "770f6a94-57f0-4282-a7d6-8883eca0ba27");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6d6faac-a023-46d2-afff-daa9c177d1a7", "AQAAAAEAACcQAAAAEEV8dBuF/utJaVJ1Fp8+3tvcLztmWBG3H00TjeCoguGihLnGwULltXFhYLwFybQlyw==", "39d54030-e261-4489-aa4f-b850e9b21b70" });
        }
    }
}
