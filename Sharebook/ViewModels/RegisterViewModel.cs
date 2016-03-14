using System.ComponentModel.DataAnnotations;

namespace Sharebook.ViewModels
{
   public class RegisterViewModel
    {
        [Required]
        [StringLength(50,MinimumLength =5)]
        []
        public string UserName { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [StringLength(250,MinimumLength =8)]
        [RegularExpression("^.*(?=.{8,})(?=.*[\\d])(?=.*[\\W]).*$",ErrorMessage ="Password must be 8 characters at least with at least 1 lower case, 1 number and 1 upper case")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password Confirmation do not match the new password")]
        public string ConfirmPassword { get; set; }
        [EmailAddressAttribute]
        public string Email { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }

    }
   
}