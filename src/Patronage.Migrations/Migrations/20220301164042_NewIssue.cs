using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.Migrations.Migrations
{
    public partial class NewIssue : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Issues",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignUserId",
                table: "Issues",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ec3b0c8-3a99-4f8c-9143-85c2e10f5d1f", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "615c64f9-38aa-455a-afb6-42834df35316", "c5967733-1582-47c0-a3df-62803ab7943c" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6ec3b0c8-3a99-4f8c-9143-85c2e10f5d1f", "1" });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ApplicationUserId",
                table: "Issues",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_ApplicationUserId",
                table: "Issues",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_ApplicationUserId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ApplicationUserId",
                table: "Issues");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6ec3b0c8-3a99-4f8c-9143-85c2e10f5d1f", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ec3b0c8-3a99-4f8c-9143-85c2e10f5d1f");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssignUserId",
                table: "Issues");

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
