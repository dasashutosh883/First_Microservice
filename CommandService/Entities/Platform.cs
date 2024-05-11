using System.ComponentModel.DataAnnotations;

namespace CommandService.Entities
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}