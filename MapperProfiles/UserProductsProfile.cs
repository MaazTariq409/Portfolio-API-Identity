using Portfolio_API.DTOs;
using Portfolio_API.Models;
using AutoMapper;

namespace Portfolio_API.MapperProfiles
{

	public class UserProductsProfile : Profile
	{
		public UserProductsProfile()
		{
			//CreateMap<UserDto, User>();
			CreateMap<UserProductsDto, UserProducts>();
			CreateMap<UserProducts, UserProductsDto>();
		}
	}

}
