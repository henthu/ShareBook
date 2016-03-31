using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharebook.Models
{
    public class SentMessage:Message
    {
        
        [Required]
        public ApplicationUser Reciever { get; set; }
    }
}
