using System.ComponentModel.DataAnnotations;

namespace Sharebook.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string OldPassword{get;set;}
        [RegularExpression("^.*(?=.{8,})(?=.*[\\d])(?=.*[\\W]).*$",ErrorMessage ="Password must be 8 characters at least with at least 1 lower case, 1 number and 1 upper case")]
        public string NewPassword{get;set;}
    }
}