﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prescription.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_Patient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                schema: "prsp",
                columns: table => new
                {
                    IdPatient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.IdPatient);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_IdPatient",
                schema: "prsp",
                table: "Prescription",
                column: "IdPatient");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                schema: "prsp",
                table: "Prescription",
                column: "IdPatient",
                principalSchema: "prsp",
                principalTable: "Patient",
                principalColumn: "IdPatient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                schema: "prsp",
                table: "Prescription");

            migrationBuilder.DropTable(
                name: "Patient",
                schema: "prsp");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_IdPatient",
                schema: "prsp",
                table: "Prescription");
        }
    }
}
