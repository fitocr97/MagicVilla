using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class Identitynombres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 9, 10, 9, 29, 38, 79, DateTimeKind.Local).AddTicks(2704), new DateTime(2024, 9, 10, 9, 29, 38, 79, DateTimeKind.Local).AddTicks(2692) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 9, 10, 9, 29, 38, 79, DateTimeKind.Local).AddTicks(2708), new DateTime(2024, 9, 10, 9, 29, 38, 79, DateTimeKind.Local).AddTicks(2707) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 9, 10, 9, 22, 22, 563, DateTimeKind.Local).AddTicks(4906), new DateTime(2024, 9, 10, 9, 22, 22, 563, DateTimeKind.Local).AddTicks(4887) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 9, 10, 9, 22, 22, 563, DateTimeKind.Local).AddTicks(4909), new DateTime(2024, 9, 10, 9, 22, 22, 563, DateTimeKind.Local).AddTicks(4908) });
        }
    }
}
