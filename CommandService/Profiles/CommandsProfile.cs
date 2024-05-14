using AutoMapper;
using CommandService.Dtos;
using CommandService.Entities;
using PlatformService;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //source -> target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest=> dest.Id,opt=>opt.Ignore());
            CreateMap<GrpcPlatformModel,Platform>()
            .ForMember(dest=>dest.ExternalID,opt=>opt.MapFrom(src=>src.PlatformId))
            .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name))
            .ForMember(dest=>dest.Commands,opt=>opt.Ignore());
        }
    }
}