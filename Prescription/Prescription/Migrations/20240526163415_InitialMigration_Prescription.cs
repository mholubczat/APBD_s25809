using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prescription.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_Prescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "prsp");

            migrationBuilder.CreateTable(
                name: "Prescription",
                schema: "prsp",
                columns: table => new
                {
                    IdPrescription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDoctor = table.Column<int>(type: "int", nullable: false),
                    IdPatient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.IdPrescription);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescription",
                schema: "prsp");
        }
    }
}
