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
            CreateMap<AdminUserServiceGetDto, UserServices>();
            CreateMap<AdminUserServicePostDto, UserServices>();
            CreateMap<AdminUserServicePostDto, UserServiceGig>();
            CreateMap<UserServiceGig, AdminUserServicePostDto>();
            CreateMap<UserServiceGigPostDto, UserServiceGig>();


            CreateMap<UserServiceGig, AdminUserServiceGigPostDto>();
            CreateMap<AdminUserServiceGigPostDto, UserServiceGig>();
        }

    }
}
