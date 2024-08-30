using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 8, 27, 12, 48, 30, 895, DateTimeKind.Local).AddTicks(2101), new DateTime(2024, 8, 27, 12, 48, 30, 895, DateTimeKind.Local).AddTicks(2090) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacio", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 8, 27, 12, 48, 30, 895, DateTimeKind.Local).AddTicks(2104), new DateTime(2024, 8, 27, 12, 48, 30, 895, DateTimeKind.Local).AddTicks(2104) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

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
    }
}
