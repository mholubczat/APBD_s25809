using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perscription.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_PerscriptionMedicament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerscriptionMedicament",
                schema: "prsp",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(type: "int", nullable: false),
                    IdPerscription = table.Column<int>(type: "int", nullable: false),
                    Dose = table.Column<int>(type: "int", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perscription_Medicament", x => new { x.IdPerscription, x.IdMedicament });
                    table.ForeignKey(
                        name: "FK_PerscriptionMedicament_Medicament_IdMedicament",
                        column: x => x.IdMedicament,
                        principalSchema: "prsp",
                        principalTable: "Medicament",
                        principalColumn: "IdMedicament");
                    table.ForeignKey(
                        name: "FK_PerscriptionMedicament_Perscription_IdPerscription",
                        column: x => x.IdPerscription,
                        principalSchema: "prsp",
                        principalTable: "Perscription",
                        principalColumn: "IdPerscription");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerscriptionMedicament_IdMedicament",
                schema: "prsp",
                table: "PerscriptionMedicament",
                column: "IdMedicament");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerscriptionMedicament",
                schema: "prsp");
        }
    }
}
