using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;

namespace MagicVilla_API
{
    public class MappingConfig : Profile //hereda de profle viene de automaper
    {
        public MappingConfig() 
        {
            CreateMap<Villa, VillaDto>(); //fuente y destino
            CreateMap<VillaDto, Villa>(); //fuente y destino

            CreateMap<Villa, VillaCreateDto>(); //fuente y destino
            CreateMap<VillaCreateDto, Villa>(); //fuente y destino

            CreateMap<Villa, VillaUpdateDto>().ReverseMap(); //en una linea


            //numero villa
            CreateMap<NumeroVilla, NumeroVillaUpdateDto>().ReverseMap(); 
            CreateMap<NumeroVilla, NumeroVillaCreateDto>().ReverseMap(); 
            CreateMap<NumeroVilla, NumeroVillaDto>().ReverseMap(); 
        }
    }
}
