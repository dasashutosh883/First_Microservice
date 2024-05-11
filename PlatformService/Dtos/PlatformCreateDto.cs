using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos
{
    public class PlatformCreateDto{
        [Required]
        public string Name { get; set; }=string.Empty;
        [Required]
        public string publisher{ get; set; }=string.Empty ;
        [Required]
        public string cost{ get; set; }=string.Empty;
    }
}