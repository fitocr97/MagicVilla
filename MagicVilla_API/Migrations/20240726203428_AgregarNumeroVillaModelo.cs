using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVillaModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 7, 26, 14, 34, 27, 669, DateTimeKind.Local).AddTicks(3707), new DateTime(2024, 7, 26, 14, 34, 27, 669, DateTimeKind.Local).AddTicks(3696) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 7, 26, 14, 34, 27, 669, DateTimeKind.Local).AddTicks(3710), new DateTime(2024, 7, 26, 14, 34, 27, 669, DateTimeKind.Local).AddTicks(3710) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "NumeroVillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 7, 18, 12, 21, 41, 140, DateTimeKind.Local).AddTicks(8467), new DateTime(2024, 7, 18, 12, 21, 41, 140, DateTimeKind.Local).AddTicks(8455) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 7, 18, 12, 21, 41, 140, DateTimeKind.Local).AddTicks(8470), new DateTime(2024, 7, 18, 12, 21, 41, 140, DateTimeKind.Local).AddTicks(8469) });
        }
    }
}
