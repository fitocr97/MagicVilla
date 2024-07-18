using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class datos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacio", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Playa", new DateTime(2024, 7, 18, 12, 21, 41, 140, DateTimeKind.Local).AddTicks(8467), new DateTime(2024, 7, 18, 12, 21, 41, 140, DateTimeKind.Local).AddTicks(8455), "", 50, "Villa Playa", 5, 500.0 },
                    { 2, "", "Montaña", new DateTime(2024, 7, 18, 12, 21, 41, 140, DateTimeKind.Local).AddTicks(8470), new DateTime(2024, 7, 18, 12, 21, 41, 140, DateTimeKind.Local).AddTicks(8469), "", 40, "Villa Montaña", 4, 400.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
