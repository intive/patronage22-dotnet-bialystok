using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patronage.MigrationsPostgre.Migrations
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
                values: new object[] { "b8d468b2-3685-4504-97ed-27f27ef55a8e", "137bcb77-6213-4d4f-a11e-cf813583ab22" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679381f2-06a1-4e22-beda-179e8e9e3236",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aa951c78-e387-4035-aeee-1f626e8fcb02", "6b3bc283-3a30-4299-9712-1ef7267ed2e4" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "90c3c7d8-0cb8-4955-8e4a-5397a1d801a5", "test2@mail.com", false, null, false, null, "TEST2@MAIL.COM", "TESTUSER2", "AQAAAAEAACcQAAAAEIR44hzbnj/pCIqsHG4vIPm/ARO5F+qPlxQp9Wjhn+EBi/q73B+RlmXZNV+yUOvgPQ==", null, false, null, "5c6f0c79-270f-4bc4-af1a-7178c9b8c185", false, "TestUser2" });
        }
    }
}