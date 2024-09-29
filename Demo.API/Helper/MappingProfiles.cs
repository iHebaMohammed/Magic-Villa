using AutoMapper;
using Demo.API.DTOs;
using Demo.DAL.Entities;

namespace Demo.API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Villa , VillaDTO>().ReverseMap();
            CreateMap<Villa , VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber , VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
