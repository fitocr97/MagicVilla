﻿// <auto-generated />
using System;
using MagicVilla_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240726203428_AgregarNumeroVillaModelo")]
    partial class AgregarNumeroVillaModelo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_API.Models.NumeroVilla", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<string>("DetalleEspecial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("NumeroVillas");
                });

            modelBuilder.Entity("MagicVilla_API.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaActualizacio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double?>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Playa",
                            FechaActualizacio = new DateTime(2024, 7, 26, 14, 34, 27, 669, DateTimeKind.Local).AddTicks(3707),
                            FechaCreacion = new DateTime(2024, 7, 26, 14, 34, 27, 669, DateTimeKind.Local).AddTicks(3696),
                            ImagenUrl = "",
                            MetrosCuadrados = 50,
                            Nombre = "Villa Playa",
                            Ocupantes = 5,
                            Tarifa = 500.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "",
                            Detalle = "Montaña",
                            FechaActualizacio = new DateTime(2024, 7, 26, 14, 34, 27, 669, DateTimeKind.Local).AddTicks(3710),
                            FechaCreacion = new DateTime(2024, 7, 26, 14, 34, 27, 669, DateTimeKind.Local).AddTicks(3710),
                            ImagenUrl = "",
                            MetrosCuadrados = 40,
                            Nombre = "Villa Montaña",
                            Ocupantes = 4,
                            Tarifa = 400.0
                        });
                });

            modelBuilder.Entity("MagicVilla_API.Models.NumeroVilla", b =>
                {
                    b.HasOne("MagicVilla_API.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
