using AutoMapper;
using AutoMapper;
using Portfolio_API.DTOs;
using Portfolio_API.DTOs.Admin;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class UserServiceProfile : Profile
    {
        public UserServiceProfile()
        {
            CreateMap<UserServiceGigDto, UserServiceGig>();

            CreateMap<UserServices, AdminUserServiceGetDto>();
            CreateMap<AdminUserServicePostDto, UserServices>();
            CreateMap<UserServiceGigPostDto, UserServiceGig>();



        }

    }
}
