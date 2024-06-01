using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perscription.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_DoctorCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perscription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Perscription");

            migrationBuilder.AddForeignKey(
                name: "FK_Perscription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Perscription",
                column: "IdDoctor",
                principalSchema: "prsp",
                principalTable: "Doctor",
                principalColumn: "IdDoctor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perscription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Perscription");

            migrationBuilder.AddForeignKey(
                name: "FK_Perscription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Perscription",
                column: "IdDoctor",
                principalSchema: "prsp",
                principalTable: "Doctor",
                principalColumn: "IdDoctor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
