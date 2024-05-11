using PlatformService.Data;
using PlatformService.Entities;
using PlatformService.Repositories.Interfaces;

namespace PlatformService.Repositories.Repository
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;
        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool CreatePlatform(Platform data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            _context.Platforms.Add(data);
            return true;
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            var item = _context.Platforms.FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                return new Platform { };
            }
        }

        public bool SaveChages()
        {
            return (_context.SaveChanges() >= 0);
        }

        bool IPlatformRepo.UpdatePlatform(Platform data)
        {
            throw new NotImplementedException();
        }
    }
}