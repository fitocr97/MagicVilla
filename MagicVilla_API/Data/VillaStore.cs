using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Data
{
    public static class VillaStore //statica
    {
        public static List<VillaDto> VillaList = new List<VillaDto>
        {
            new VillaDto {Id=1,Nombre="Mar", Ocupantes= 20, MetrosCuadrados= 50},
            new VillaDto {Id=2,Nombre="Montaña", Ocupantes= 10, MetrosCuadrados= 25}
        };
    }
}
