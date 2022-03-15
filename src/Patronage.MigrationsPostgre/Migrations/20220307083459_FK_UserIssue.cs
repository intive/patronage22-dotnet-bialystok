using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.MigrationsPostgre.Migrations
{
    public partial class FK_UserIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "AssignUserId",
                table: "Issues",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23aedcb1-afc3-4da6-805b-5c6174576a6a", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "90c69ed4-fb0d-4e85-8201-c85183d0b1a7", "d4b1e76d-89cf-456a-a2a7-5c3686fc72ea" });

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignUserId",
                value: "1");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "23aedcb1-afc3-4da6-805b-5c6174576a6a", "1" });

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
                keyValues: new object[] { "23aedcb1-afc3-4da6-805b-5c6174576a6a", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23aedcb1-afc3-4da6-805b-5c6174576a6a");

            migrationBuilder.AlterColumn<int>(
                name: "AssignUserId",
                table: "Issues",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Issues",
                type: "text",
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

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignUserId",
                value: null);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "17388f4b-4c67-4e99-a515-5c987844896c", "1" });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ApplicationUserId",
                table: "Issues",
                column: "ApplicationUserId");

        }
    }
}
