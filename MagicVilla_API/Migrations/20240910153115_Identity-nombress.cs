using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class Identitynombress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 9, 10, 9, 31, 15, 491, DateTimeKind.Local).AddTicks(6332), new DateTime(2024, 9, 10, 9, 31, 15, 491, DateTimeKind.Local).AddTicks(6316) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 9, 10, 9, 31, 15, 491, DateTimeKind.Local).AddTicks(6402), new DateTime(2024, 9, 10, 9, 31, 15, 491, DateTimeKind.Local).AddTicks(6401) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "AspNetUsers");

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
    }
}
