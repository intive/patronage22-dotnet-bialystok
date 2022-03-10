using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.Migrations.Migrations
{
    public partial class FK_UserIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "AssignUserId",
                table: "Issues",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab59b070-bb62-41a5-928e-0f91132472e3", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9427f7c4-c118-4e4f-9332-bd852d4c9d64", "05bc821a-598e-4d02-aca5-61dd3539d277" });

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignUserId",
                value: "1");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ab59b070-bb62-41a5-928e-0f91132472e3", "1" });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignUserId",
                table: "Issues",
                column: "AssignUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_AssignUserId",
                table: "Issues",
                column: "AssignUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_AssignUserId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssignUserId",
                table: "Issues");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ab59b070-bb62-41a5-928e-0f91132472e3", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab59b070-bb62-41a5-928e-0f91132472e3");

            migrationBuilder.AlterColumn<int>(
                name: "AssignUserId",
                table: "Issues",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Issues",
                type: "nvarchar(450)",
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

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignUserId",
                value: null);

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
    }
}
