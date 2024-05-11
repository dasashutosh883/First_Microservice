using PlatformService.Dtos;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpclient;
        private readonly IConfiguration _config;
        public HttpCommandDataClient(HttpClient client, IConfiguration config)
        {
            _httpclient = client;
            _config = config;
        }

        public async Task SendPlatformToCommand(PlatformReadDto dto)
        {
            var httpContent=new StringContent(
                JsonConvert.SerializeObject(dto),
                Encoding.UTF8,
                "application/json"
            );
            var response=await _httpclient.PostAsync(_config["CommandService"],httpContent);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("-> sync Post to command service is ok!");
            }
            else
            {
                Console.WriteLine("-> sync Post to command service is  not ok!");
            }
        }
    }
}