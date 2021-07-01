using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Farsight.IdentityService.Migrations
{
    public partial class AddProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "bytea",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "2d34754f-fab3-4237-abfb-fe9a71071379");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "121a086c-e2f1-45aa-bfab-09c12cc0dc5a", "AQAAAAEAACcQAAAAENGscKUcE6Yd2xa9MRZXEQ+3ezOg4RotTkx5DszGliyhDUPG8TBrcDaX5sAC+dwSsA==", "bb4fa1ef-7e4f-4b57-9097-a04d0a4ffe86" });
        }
    }
}
