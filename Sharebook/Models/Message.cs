using System;
using System.ComponentModel.DataAnnotations;

namespace Sharebook.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public DateTime SendDate { get; set; }
        [Required]
        public string Content { get; set; }
        public bool isRead { get; set; } = false;
        
    }
}