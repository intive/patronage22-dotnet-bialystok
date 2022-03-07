using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.MigrationsPostgre.Migrations
{
    public partial class adds_ids_to_projects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2f022446-1ba9-4920-837f-3b2b4cdd768a", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f022446-1ba9-4920-837f-3b2b4cdd768a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "afceb0fa-dd8b-4aa7-9242-f8c3156d11fd", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "33bcc014-584e-4d4d-9fbc-dfa3e3a27d18", "4a8cf4ba-aaed-4832-8a3f-1c9d9384a220" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "afceb0fa-dd8b-4aa7-9242-f8c3156d11fd", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "afceb0fa-dd8b-4aa7-9242-f8c3156d11fd", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afceb0fa-dd8b-4aa7-9242-f8c3156d11fd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f022446-1ba9-4920-837f-3b2b4cdd768a", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "da88d64d-a626-4d71-bb30-4d8460c7463c", "e9bd2550-4579-4be6-8810-68ff6972cdf2" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2f022446-1ba9-4920-837f-3b2b4cdd768a", "1" });
        }
    }
}
