using CommandService.Entities;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            _context.commands.Add(command);
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            _context.platforms.Add(plat);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _context.platforms.Any(p => p.ExternalId == externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.commands
                .Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault()!;
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.platform.Name);
        }

        public bool PlaformExits(int platformId)
        {
            return _context.platforms.Any(p => p.Id == platformId);
        }
    }
}