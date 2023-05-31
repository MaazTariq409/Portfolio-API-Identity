using AutoMapper;
using Portfolio_API.DTOs;
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

            CreateMap<UserBlogs , AdminBlogsDto>();
            CreateMap<AdminBlogsDto , UserBlogs>();

            CreateMap<AdminBlogGetDto, UserBlogs>();
            CreateMap<UserBlogs, AdminBlogsDto>();
            CreateMap<UserBlogs, AdminBlogPostDto>();
            CreateMap<AdminBlogPostDto, UserBlogs>();

            CreateMap<AdminProductGetDto, UserProducts>();
            CreateMap<UserProducts, AdminProductGetDto>();
            CreateMap<UserProducts, AdminProductPostDto>();
            CreateMap<AdminProductPostDto, UserProducts>();
            CreateMap<UserProducts, AdminProductDto>();
            CreateMap<AdminProductDto, UserProducts>();

        }
    }
}
