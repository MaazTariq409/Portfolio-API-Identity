using AutoMapper;
using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<UserCountry, CountryDto>();

            CreateMap<CountryDto, UserCountry>();
        }

    }
}
