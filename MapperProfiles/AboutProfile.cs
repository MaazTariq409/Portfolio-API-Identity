using AutoMapper;
using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class AboutProfile : Profile
    {
        public AboutProfile()
        {
            CreateMap<UserProfile, AboutDto>();
            CreateMap<AboutDto, UserProfile>();
        }
    }
}
