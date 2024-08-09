using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class hola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Tarifa",
                table: "Villas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DetalleEspecial",
                table: "NumeroVillas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 8, 7, 14, 50, 33, 514, DateTimeKind.Local).AddTicks(1349), new DateTime(2024, 8, 7, 14, 50, 33, 514, DateTimeKind.Local).AddTicks(1338) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 8, 7, 14, 50, 33, 514, DateTimeKind.Local).AddTicks(1354), new DateTime(2024, 8, 7, 14, 50, 33, 514, DateTimeKind.Local).AddTicks(1353) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Tarifa",
                table: "Villas",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DetalleEspecial",
                table: "NumeroVillas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
