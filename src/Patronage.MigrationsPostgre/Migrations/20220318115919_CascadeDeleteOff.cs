using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.MigrationsPostgre.Migrations
{
    public partial class CascadeDeleteOff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardsStatus_Boards_BoardId",
                table: "BoardsStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardsStatus_Statuses_StatusId",
                table: "BoardsStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_ApplicationUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Issues_IssueId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679381f2-06a1-4e22-beda-179e8e9e3236",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "62c6117c-688e-430d-a50c-4934c95b496c", "417ba7f5-b8c2-4004-824f-6873732e5174" });

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsStatus_Boards_BoardId",
                table: "BoardsStatus",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsStatus_Statuses_StatusId",
                table: "BoardsStatus",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_ApplicationUserId",
                table: "Comment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Issues_IssueId",
                table: "Comment",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardsStatus_Boards_BoardId",
                table: "BoardsStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardsStatus_Statuses_StatusId",
                table: "BoardsStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_ApplicationUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Issues_IssueId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679381f2-06a1-4e22-beda-179e8e9e3236",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "60817d88-9d9a-4d15-92e8-ab06239721e3", "3b0df98f-34f6-4c41-bdad-8fde3eef969e" });

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsStatus_Boards_BoardId",
                table: "BoardsStatus",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsStatus_Statuses_StatusId",
                table: "BoardsStatus",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_ApplicationUserId",
                table: "Comment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Issues_IssueId",
                table: "Comment",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
