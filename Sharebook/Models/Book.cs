using System;
using System.ComponentModel.DataAnnotations;

namespace Sharebook.Models
{
    public class Book
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Key]
        public int Id {get;set;}
        
    }
    
}