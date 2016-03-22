using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharebook.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public string UserName { get; set; }
        public bool isRead { get; set; } = false;

        public string UserId{ get; set; }
        public DateTime CreatedAt { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}