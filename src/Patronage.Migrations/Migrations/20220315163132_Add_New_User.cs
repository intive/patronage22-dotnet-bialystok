using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.Migrations.Migrations
{
    public partial class Add_New_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ab59b070-bb62-41a5-928e-0f91132472e3", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab59b070-bb62-41a5-928e-0f91132472e3");

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecondName", "SecurityStamp", "UserName" },
                values: new object[] { "31d02bbe-b886-4ae9-bed9-7f68f194a2d6", "test2@mail.com", null, "TEST2@MAIL.COM", "TESTUSER2", "AQAAAAEAACcQAAAAEIR44hzbnj/pCIqsHG4vIPm/ARO5F+qPlxQp9Wjhn+EBi/q73B+RlmXZNV+yUOvgPQ==", null, "98e970ed-485b-4aa0-bf6c-fac3670cc7d4", "TestUser2" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "679381f2-06a1-4e22-beda-179e8e9e3236", 0, "f80a4385-14c0-4592-a7f8-7d2551fc7351", "test1@mail.com", false, null, false, null, "TEST1@MAIL.COM", "TESTUSER1", "AQAAAAEAACcQAAAAEIR44hzbnj/pCIqsHG4vIPm/ARO5F+qPlxQp9Wjhn+EBi/q73B+RlmXZNV+yUOvgPQ==", null, false, null, "aa93b3a4-af52-4a79-8f81-9ae0be419dee", false, "TestUser1" });

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
                values: new object[] { "ab59b070-bb62-41a5-928e-0f91132472e3", "1", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecondName", "SecurityStamp", "UserName" },
                values: new object[] { "9427f7c4-c118-4e4f-9332-bd852d4c9d64", null, "FirstTestFirstname", null, null, null, "FirstTestSurname", "05bc821a-598e-4d02-aca5-61dd3539d277", null });

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
        }
    }
}
