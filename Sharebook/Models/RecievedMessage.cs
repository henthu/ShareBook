using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharebook.Models
{
    public class RecievedMessage :Message
    {
        
        [Required]
        
        public ApplicationUser Sender { get; set; }
    }
}
