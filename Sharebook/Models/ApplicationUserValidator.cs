using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharebook.Models
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        private ApplicationDbContext _context;

        public ApplicationUserValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(user => user.UserName).Must(IsUniqueUserName).WithMessage("Username already Exists");
        }
        private bool IsUniqueUserName(string userName)
        {

            return false;
        }


    }
}
