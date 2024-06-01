using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prescription.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_DoctorCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Prescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Prescription",
                column: "IdDoctor",
                principalSchema: "prsp",
                principalTable: "Doctor",
                principalColumn: "IdDoctor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Prescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Prescription",
                column: "IdDoctor",
                principalSchema: "prsp",
                principalTable: "Doctor",
                principalColumn: "IdDoctor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
