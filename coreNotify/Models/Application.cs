using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace coreNotify.Models
{
    public class Application
    {
        [Key]
        public int AppId { get; set; }
        [Required]
        [DisplayName("Internal Name")]
        public string AppName { get; set; }
        [Required]
        [DisplayName("Displayed Name")]
        public string DisplayName { get; set; }
    }
}
