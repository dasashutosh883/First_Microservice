using System.ComponentModel.DataAnnotations;

namespace PlatformService.Entities
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }=string.Empty;
        [Required]
        public string publisher{ get; set; }=string.Empty ;
        [Required]
        public string cost{ get; set; }=string.Empty;
        public DateTime createdon{ get; set; }=DateTime.Now;
    }
}