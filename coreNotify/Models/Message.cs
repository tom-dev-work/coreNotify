using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreNotify.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [DisplayName("Message Content")]
        public string Content { get; set; }
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        [DisplayName("App")]
        [ForeignKey("AppId")]
        [Required]
        public int AppId { get; set; }
    }
}
