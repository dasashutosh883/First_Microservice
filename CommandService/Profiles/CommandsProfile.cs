using AutoMapper;
using CommandService.Dtos;
using CommandService.Entities;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //source -> target
            CreateMap<Platform,PlatformReadDto>();
            CreateMap<CommandCreateDto,Command>();
            CreateMap<Command,CommandReadDto>();
        }
    }
}