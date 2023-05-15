using AutoMapper;
using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class UserExperienceProfile : Profile
    {
        public UserExperienceProfile()
        {
            CreateMap<UserExperience, UserExperienceDto>();

            CreateMap<UserExperienceDto, UserExperience>();
        }

    }
}
