﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class VillaCreateDto
    {
        [Required]
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        [Required]
        public double? Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Amenidad { get; set; }
    }
}
