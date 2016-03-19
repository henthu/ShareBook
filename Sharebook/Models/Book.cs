using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sharebook.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        
        public string Genre { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }

}