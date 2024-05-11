using PlatformService.Entities;

namespace PlatformService.Repositories.Interfaces
{
    public interface IPlatformRepo{
        bool SaveChages();
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        bool CreatePlatform(Platform data);
        bool UpdatePlatform(Platform data);
    }
}
