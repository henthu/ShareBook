using System;
using System.ComponentModel.DataAnnotations;

namespace Sharebook.ViewModels
{
   public class UserViewModel
    {
        [Required]
        [StringLength(50,MinimumLength =5)]
        public string Login { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [StringLength(250,MinimumLength =8)]
        [RegularExpression()]
        public string Password { get; set; }
        public string Email { get; set; }

    }
   
}