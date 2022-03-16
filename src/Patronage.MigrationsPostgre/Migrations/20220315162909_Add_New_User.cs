using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.MigrationsPostgre.Migrations
{
    public partial class Add_New_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "23aedcb1-afc3-4da6-805b-5c6174576a6a", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23aedcb1-afc3-4da6-805b-5c6174576a6a");

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "AspNetUserTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecondName", "SecurityStamp", "UserName" },
                values: new object[] { "90c3c7d8-0cb8-4955-8e4a-5397a1d801a5", "test2@mail.com", null, "TEST2@MAIL.COM", "TESTUSER2", "AQAAAAEAACcQAAAAEIR44hzbnj/pCIqsHG4vIPm/ARO5F+qPlxQp9Wjhn+EBi/q73B+RlmXZNV+yUOvgPQ==", null, "5c6f0c79-270f-4bc4-af1a-7178c9b8c185", "TestUser2" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "679381f2-06a1-4e22-beda-179e8e9e3236", 0, "aa951c78-e387-4035-aeee-1f626e8fcb02", "test1@mail.com", false, null, false, null, "TEST1@MAIL.COM", "TESTUSER1", "AQAAAAEAACcQAAAAEIR44hzbnj/pCIqsHG4vIPm/ARO5F+qPlxQp9Wjhn+EBi/q73B+RlmXZNV+yUOvgPQ==", null, false, null, "6b3bc283-3a30-4299-9712-1ef7267ed2e4", false, "TestUser1" });

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignUserId",
                value: "679381f2-06a1-4e22-beda-179e8e9e3236");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679381f2-06a1-4e22-beda-179e8e9e3236");

            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "AspNetUserTokens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23aedcb1-afc3-4da6-805b-5c6174576a6a", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecondName", "SecurityStamp", "UserName" },
                values: new object[] { "90c69ed4-fb0d-4e85-8201-c85183d0b1a7", null, "FirstTestFirstname", null, null, null, "FirstTestSurname", "d4b1e76d-89cf-456a-a2a7-5c3686fc72ea", null });

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
        }
    }
}
