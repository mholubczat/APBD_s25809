using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prescription.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_PrescriptionMedicament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrescriptionMedicament",
                schema: "prsp",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(type: "int", nullable: false),
                    IdPrescription = table.Column<int>(type: "int", nullable: false),
                    Dose = table.Column<int>(type: "int", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription_Medicament", x => new { x.IdPrescription, x.IdMedicament });
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicament_Medicament_IdMedicament",
                        column: x => x.IdMedicament,
                        principalSchema: "prsp",
                        principalTable: "Medicament",
                        principalColumn: "IdMedicament");
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicament_Prescription_IdPrescription",
                        column: x => x.IdPrescription,
                        principalSchema: "prsp",
                        principalTable: "Prescription",
                        principalColumn: "IdPrescription");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicament_IdMedicament",
                schema: "prsp",
                table: "PrescriptionMedicament",
                column: "IdMedicament");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionMedicament",
                schema: "prsp");
        }
    }
}
