using System;
using System.ComponentModel.DataAnnotations;

namespace Sharebook.ViewModels
{
    public class MessageViewModel
    {
        public string Id { get; set; }
        [Required]
        public string content { get; set; }
        
        [Required]
        public string RecieverUserName { get; set; }
        public bool isRead { get; set; }
        public DateTime SendDate { get; set; }
    }
}