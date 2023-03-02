using System.ComponentModel.DataAnnotations;

namespace coreNotify.Models
{
    public class Application
    {
        [Key]
        public int AppId { get; set; }
        [Required]
        public string AppName { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
