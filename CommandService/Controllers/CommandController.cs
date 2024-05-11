using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController, Route("api/cmd/platform/{platformid}/[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public CommandController(ICommandRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsPerPlatform(int platformid)
        {
            if (!_repo.PlaformExits(platformid))
            {
                return NotFound();
            }
            var commands = _repo.GetCommandsForPlatform(platformid);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }
        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformid, int commandId)
        {

            if (!_repo.PlaformExits(platformid))
            {
                return NotFound();
            }

            var command = _repo.GetCommand(platformid, commandId);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformid, CommandCreateDto commandDto)
        {

            if (!_repo.PlaformExits(platformid))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _repo.CreateCommand(platformid, command);
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new { platformId = platformid, commandId = commandReadDto.Id }, commandReadDto);
        }




    }
}