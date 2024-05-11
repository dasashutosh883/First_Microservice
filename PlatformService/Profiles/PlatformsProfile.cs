using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Entities;

namespace PlatformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            //source -> target
            CreateMap<Platform,PlatformReadDto>();
            CreateMap<PlatformCreateDto,Platform>();
            CreateMap<PlatformReadDto,PlatformPublishedDto>();
        }
    }
}