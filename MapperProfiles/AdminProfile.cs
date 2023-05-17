using AutoMapper;
using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Education, AdminEducationDto>();
            CreateMap<AdminEducationDto, Education>();
            CreateMap<Skills, adminSkillDto>();
            CreateMap<adminSkillDto, Skills>();
            CreateMap<UserExperience, AdminExperienceDto>();
            CreateMap<AdminExperienceDto, UserExperience>();
        }
    }
}
