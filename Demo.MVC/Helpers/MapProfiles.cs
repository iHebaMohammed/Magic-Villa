using AutoMapper;
using Demo.API.DTOs;

namespace Demo.MVC.Helpers
{
	public class MapProfiles : Profile
	{
		public MapProfiles() 
		{
			CreateMap<VillaDTO , VillaCreateDTO>().ReverseMap();
			CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
			CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
			CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
		}
	}
}
