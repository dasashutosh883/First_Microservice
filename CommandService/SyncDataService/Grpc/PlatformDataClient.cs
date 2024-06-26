using AutoMapper;
using CommandService.Entities;
using Grpc.Net.Client;
using PlatformService;

namespace CommandService.SyncDataService.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration config,IMapper mapper)
        {
            _config=config;
            _mapper=mapper;
        }
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"--> calling grpc Service {_config["GrpcPlatform"]}");
            var channel=GrpcChannel.ForAddress(_config["GrpcPlatform"]);
            var client=new GrpcPlatform.GrpcPlatformClient(channel);
            var request=new GetAllRequest();

            try
            {
                var reply=client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
            }
            catch (Exception ex){
                Console.WriteLine($"-->error calling grpc service{ex.Message}");
                return null;
            }
        }
    }
}