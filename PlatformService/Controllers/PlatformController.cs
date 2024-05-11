using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Dtos;
using PlatformService.Entities;
using PlatformService.Repositories.Interfaces;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _cmdClient;
        private readonly IMessageBusClient _busClient;

        public PlatformController(IPlatformRepo repo, IMapper mapper,ICommandDataClient cmdClient,IMessageBusClient busClient)
        {
            _repo = repo;
            _mapper = mapper;
            _cmdClient = cmdClient;
            _busClient=busClient;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Platform>> Get()
        {
            var records = _repo.GetAllPlatforms();
            if (records.Any())
                return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(records));
            return NotFound();
        }
        [HttpGet("{id}",Name="GetOne")]
        public ActionResult<Platform> GetOne(int id)
        {
            var record = _repo.GetPlatformById(id);
            if (record != null)
                return Ok(_mapper.Map<PlatformReadDto>(record));
            return NotFound();
        }
        [HttpPost]
        public ActionResult<PlatformCreateDto> CreatePlatform(PlatformCreateDto data)
        {
            var PlatformData = _mapper.Map<Platform>(data);
            
            bool ret = _repo.CreatePlatform(PlatformData);
            if (ret)
                _repo.SaveChages();
                var retvalues=_mapper.Map<PlatformReadDto>(PlatformData);
                //send async message
                try
                {
                    var platformPublished=_mapper.Map<PlatformPublishedDto>(retvalues);
                    platformPublished.Event="Platform Published";
                    _busClient.PublishNewPlatform(platformPublished);
                }
                catch(Exception ex){
                    Console.WriteLine($"--> error sending msg {ex.Message}");
                }
                return CreatedAtRoute(nameof(GetOne),new {Id=retvalues.Id},retvalues);
               
        }
        [HttpGet("status")]
        public IActionResult Status(){
            return Ok(new ApiResponse{statuscode=200,status="sucess",message="Api is running "});
        }
    }
}