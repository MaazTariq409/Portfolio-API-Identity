using AutoMapper;
using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class UserBlogProfile : Profile
    {
        public UserBlogProfile()
        {
            CreateMap<UserBlogs , UserBlogsDto>();
            CreateMap<UserBlogsDto , UserBlogs>();
            CreateMap<UserBlogDto , UserBlogs>();
            CreateMap<UserBlogs , UserBlogDto>();

        }

    }
}
