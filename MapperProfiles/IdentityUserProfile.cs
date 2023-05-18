using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
	public class IdentityUserProfile : Profile
	{
		public IdentityUserProfile()
		{
			CreateMap<IdentityManual, IdentityUserDto>();
			CreateMap<IdentityUserDto, IdentityManual>();
		}
	}
}
