using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.MigrationsPostgre.Migrations
{
    public partial class NewIssues : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Issues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignUserId",
                table: "Issues",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "17388f4b-4c67-4e99-a515-5c987844896c", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7deae26b-c64d-4402-8a06-5e4daed421f6", "620cea54-2bd1-46ab-8f96-398b5415de6b" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "17388f4b-4c67-4e99-a515-5c987844896c", "1" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "17388f4b-4c67-4e99-a515-5c987844896c", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17388f4b-4c67-4e99-a515-5c987844896c");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssignUserId",
                table: "Issues");

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
