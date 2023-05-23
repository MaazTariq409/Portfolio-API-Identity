using AutoMapper;
using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.MapperProfiles
{
    public class InstituteProfile : Profile
    {
        public InstituteProfile()
        {
            CreateMap<UserInstitute, InstituteDto>();

            CreateMap<InstituteDto, UserInstitute>();
        }

    }
}
