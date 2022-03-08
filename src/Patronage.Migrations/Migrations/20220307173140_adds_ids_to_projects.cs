using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.Migrations.Migrations
{
    public partial class adds_ids_to_projects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "524901bb-f19a-46fe-b8cd-4bb5cf3be65a", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "524901bb-f19a-46fe-b8cd-4bb5cf3be65a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f0d9462-98c8-4779-aaef-9e5e2846bddd", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "581e634d-5666-40c6-9452-15dad02283a1", "1427e670-e8e7-4d57-96d4-8e027a0a850c" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6f0d9462-98c8-4779-aaef-9e5e2846bddd", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6f0d9462-98c8-4779-aaef-9e5e2846bddd", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f0d9462-98c8-4779-aaef-9e5e2846bddd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "524901bb-f19a-46fe-b8cd-4bb5cf3be65a", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "777a836a-34a7-4b03-a55c-9f14c6037c8c", "974dc344-1a7e-4c77-9066-312c6ebc8f32" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "524901bb-f19a-46fe-b8cd-4bb5cf3be65a", "1" });
        }
    }
}
