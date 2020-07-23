using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMedXUtilities.Migrations
{
    public partial class IMedXMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "IMedXPatientData",
                schema: "dbo",
                columns: table => new
                {
                    PA = table.Column<string>(nullable: true),
                    DOC = table.Column<string>(nullable: true),
                    ICD = table.Column<string>(nullable: true),
                    NDC = table.Column<string>(nullable: true),
                    AMT = table.Column<double>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "InputPatientICD",
                schema: "dbo",
                columns: table => new
                {
                    PA = table.Column<string>(nullable: true),
                    DOC = table.Column<string>(nullable: true),
                    ICD = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "InputPatientNDC",
                schema: "dbo",
                columns: table => new
                {
                    PA = table.Column<string>(nullable: true),
                    NDC = table.Column<string>(nullable: true),
                    AMT = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IMedXPatientData",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InputPatientICD",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InputPatientNDC",
                schema: "dbo");
        }
    }
}
