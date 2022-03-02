using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.MigrationsPostgre.Migrations
{
    public partial class AddTestDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f022446-1ba9-4920-837f-3b2b4cdd768a", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "da88d64d-a626-4d71-bb30-4d8460c7463c", null, false, "FirstTestFirstname", false, null, null, null, null, null, false, "FirstTestSurname", "e9bd2550-4579-4be6-8810-68ff6972cdf2", false, null });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Alias", "CreatedOn", "Description", "IsActive", "ModifiedOn", "Name", "ProjectId" },
                values: new object[,]
                {
                    { 1, "1st board", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a description of first test board", true, null, "First test Board", 1 },
                    { 2, "2nd board", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a description of second test board", false, null, "Second test Board", 1 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Alias", "CreatedOn", "Description", "IsActive", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, "1st", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a description of first test project", true, null, "First project" },
                    { 2, "2nd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a description of 2nd test project", false, null, "Second test project" },
                    { 3, "3rd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, "Third test project" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { 1, "TO DO" },
                    { 2, "IN PROGRESS" },
                    { 3, "DONE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2f022446-1ba9-4920-837f-3b2b4cdd768a", "1" });

            migrationBuilder.InsertData(
                table: "BoardsStatus",
                columns: new[] { "BoardId", "StatusId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "Alias", "BoardId", "CreatedOn", "Description", "IsActive", "ModifiedOn", "Name", "ProjectId", "StatusId" },
                values: new object[,]
                {
                    { 1, "1st issue", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a description of first test issue. This Issue is connected to a Board", true, null, "First test Issue", 1, 1 },
                    { 2, "2nd issue", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a description of second test issue. This Issue isn't connected to a Board", true, null, "Second test Issue", 1, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2f022446-1ba9-4920-837f-3b2b4cdd768a", "1" });

            migrationBuilder.DeleteData(
                table: "BoardsStatus",
                keyColumns: new[] { "BoardId", "StatusId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BoardsStatus",
                keyColumns: new[] { "BoardId", "StatusId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "BoardsStatus",
                keyColumns: new[] { "BoardId", "StatusId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f022446-1ba9-4920-837f-3b2b4cdd768a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
