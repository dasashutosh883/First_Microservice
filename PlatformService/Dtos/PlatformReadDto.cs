
namespace PlatformService.Dtos
{
    public class PlatformReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string publisher { get; set; } = string.Empty;
        public string cost { get; set; } = string.Empty;
        public DateTime createdon { get; set; }
    }
}