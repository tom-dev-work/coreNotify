using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreNotify.Models
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppId { get; set; }
        [Required]
        [DisplayName("Internal Name")]
        public string AppName { get; set; }
        [Required]
        [DisplayName("Displayed Name")]
        public string DisplayName { get; set; }
    }
}
