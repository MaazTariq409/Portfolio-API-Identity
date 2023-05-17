using AutoMapper;
using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<UserCity, CityDto>();

            CreateMap<CityDto, UserCity>();
        }

    }
}
