using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController,Route("api/cmd/[controller]")]
    public class PlatformController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandRepo _repo;

        public PlatformController(IMapper mapper,ICommandRepo repo)
        {
            _mapper=mapper;
            _repo=repo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platform=_repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platform));
        }

        [HttpGet("status")]
        public ActionResult GetStatus(){
            return Ok(new ApiResponse{statuscode=200,status="sucess",message="Api is running "});
        }
    }
}