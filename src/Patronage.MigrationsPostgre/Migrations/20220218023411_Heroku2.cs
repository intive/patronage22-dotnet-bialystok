using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Patronage.MigrationsPostgre.Migrations
{
    public partial class Heroku2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Boards",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Alias = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(1024)", unicode: false, maxLength: 1024, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", unicode: false, maxLength: 1024, nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MachineName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Logged = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Level = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "character varying(1024)", unicode: false, maxLength: 1024, nullable: false),
                    Logger = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: true),
                    Callsite = table.Column<string>(type: "text", unicode: false, nullable: true),
                    Exception = table.Column<string>(type: "character varying(1024)", unicode: false, maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Alias = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(1024)", unicode: false, maxLength: 1024, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", unicode: false, maxLength: 1024, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Alias = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(1024)", unicode: false, maxLength: 1024, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", unicode: false, maxLength: 1024, nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    BoardId = table.Column<int>(type: "integer", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Boards_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "public",
                        principalTable: "Boards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_BoardId",
                schema: "public",
                table: "Issues",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                schema: "public",
                table: "Issues",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Logs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Boards",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "public");
        }
    }
}
