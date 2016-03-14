using System.ComponentModel.DataAnnotations;

namespace Sharebook.Models
{
    public class City
    {
        [Key]
        public int Id{get;set;}
        public string Name{get;set;}
        public string CountryCode{get;set;}
        
    }
}