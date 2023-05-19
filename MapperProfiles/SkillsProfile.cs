using Portfolio_API.DTOs;
using Portfolio_API.Models;
using AutoMapper;

namespace Portfolio_API.MapperProfiles
{

	public class SkillsProfile : Profile
	{
		public SkillsProfile()
		{
			CreateMap<UserDto, UserProfile>();
			CreateMap<SkillsDto, Skills>();
			CreateMap<Skills, SkillsDto>();
		}
	}

}
