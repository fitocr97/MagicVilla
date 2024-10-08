﻿using MagicVilla_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserApp>
    {
        //con esto queda lista la configuracion de EF y DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //base dbContext, va indicar el padre y le enviamos toda la config 
        {
            
        }
        public DbSet<UserApp> UsersApp { get; set; }
        public DbSet<Villa> Villas {  get; set; } //modelo creado en bd
        public DbSet<User> Users {  get; set; }
        public DbSet<NumeroVilla> NumeroVillas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//esto para que todo se relacione con identity
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre="Villa Playa",
                    Detalle= "Playa",
                    ImagenUrl= "",
                    Ocupantes= 5,
                    MetrosCuadrados= 50,
                    Tarifa= 500,
                    Amenidad="",
                    FechaCreacion= DateTime.Now,
                    FechaActualizacio= DateTime.Now
                },
                 new Villa()
                 {
                     Id = 2,
                     Nombre = "Villa Montaña",
                     Detalle = "Montaña",
                     ImagenUrl = "",
                     Ocupantes = 4,
                     MetrosCuadrados = 40,
                     Tarifa = 400,
                     Amenidad = "",
                     FechaCreacion = DateTime.Now,
                     FechaActualizacio = DateTime.Now
                 }
            );

        }
    }
}
