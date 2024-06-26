﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prescription.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_Doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                schema: "prsp",
                columns: table => new
                {
                    IdDoctor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.IdDoctor);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_IdDoctor",
                schema: "prsp",
                table: "Prescription",
                column: "IdDoctor");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                schema: "prsp",
                table: "Prescription");

            migrationBuilder.DropTable(
                name: "Doctor",
                schema: "prsp");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_IdDoctor",
                schema: "prsp",
                table: "Prescription");
        }
    }
}
