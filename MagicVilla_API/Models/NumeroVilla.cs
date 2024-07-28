using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace MagicVilla_API.Models
{
    public class NumeroVilla
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]  //quita el autoincremental y hay que poner el id
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        [ForeignKey("VillaId")]
        public Villa Villa { get; set; }

        public String DetalleEspecial { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
