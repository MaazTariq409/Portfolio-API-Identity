using Portfolio_API.DTOs;
using Portfolio_API.Models;
using AutoMapper;

namespace Portfolio_API.MapperProfiles
{

	public class UserProjectsProfile : Profile
	{
		public UserProjectsProfile()
		{
			//CreateMap<UserDto, User>();
			CreateMap<UserProjectsDto, UserProjects>();
			CreateMap<UserProjects, UserProjectsDto>();
		}
	}

}
