using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgdtTestTask.Medical.DataAccess.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientName",
                columns: table => new
                {
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Use = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Family = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientName", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_PatientName_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientGivenName",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientGivenName", x => new { x.PatientId, x.Value });
                    table.ForeignKey(
                        name: "FK_PatientGivenName_PatientName_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientName",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Birthdate",
                table: "Patient",
                column: "Birthdate");

            migrationBuilder.CreateIndex(
                name: "IX_PatientName_Id",
                table: "PatientName",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientGivenName");

            migrationBuilder.DropTable(
                name: "PatientName");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
