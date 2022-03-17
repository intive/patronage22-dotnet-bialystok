using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.Migrations.Migrations
{
    public partial class Delete_Old_Seeded_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679381f2-06a1-4e22-beda-179e8e9e3236",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c93346d2-9d8e-40fe-9895-908d71d72833", "86200e79-87cb-43ae-b99e-c27037fa547b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679381f2-06a1-4e22-beda-179e8e9e3236",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f80a4385-14c0-4592-a7f8-7d2551fc7351", "aa93b3a4-af52-4a79-8f81-9ae0be419dee" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "31d02bbe-b886-4ae9-bed9-7f68f194a2d6", "test2@mail.com", false, null, false, null, "TEST2@MAIL.COM", "TESTUSER2", "AQAAAAEAACcQAAAAEIR44hzbnj/pCIqsHG4vIPm/ARO5F+qPlxQp9Wjhn+EBi/q73B+RlmXZNV+yUOvgPQ==", null, false, null, "98e970ed-485b-4aa0-bf6c-fac3670cc7d4", false, "TestUser2" });
        }
    }
}