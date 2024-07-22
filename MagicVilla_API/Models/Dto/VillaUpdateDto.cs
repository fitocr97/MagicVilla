﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Detalle { get; set; }
        [Required]
        public double? Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }
        [Required]
        public string? ImagenUrl { get; set; }
        [Required]
        public string? Amenidad { get; set; }
        //public DateTime? FechaCreacion { get; set; }
        //public DateTime? FechaActualizacio { get; set; }
    }
}
