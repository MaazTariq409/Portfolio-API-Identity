using AutoMapper;
using Portfolio_API.DTOs.Admin;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Education, AdminPostEducationDto>();
            CreateMap<AdminPostEducationDto, Education>();

            CreateMap<Skills, AdminPostSkillDto>();
            CreateMap<AdminPostSkillDto, Skills>();

            CreateMap<UserExperience, AdminPostExperienceDto>();
            CreateMap<AdminPostExperienceDto, UserExperience>();

            CreateMap<AdminGetEducation, Education>();
            CreateMap<Education, AdminGetEducation>();

            CreateMap<AdminGetSkillDto, Skills>();
            CreateMap<Skills, AdminGetSkillDto>();
        }
    }
}
