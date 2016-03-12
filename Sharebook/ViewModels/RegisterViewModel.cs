using System.ComponentModel.DataAnnotations;

namespace Sharebook.ViewModels
{
   public class RegisterViewModel
    {
        [Required]
        [StringLength(50,MinimumLength =5)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [StringLength(250,MinimumLength =8)]
        [RegularExpression("^.*(?=.{8,})(?=.*[\\d])(?=.*[\\W]).*$")]
        public string Password { get; set; }
        [EmailAddressAttribute]
        public string Email { get; set; }

    }
   
}