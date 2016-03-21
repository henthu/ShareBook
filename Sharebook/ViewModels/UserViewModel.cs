using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharebook.ViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public ICollection<BookViewModel> Books { get; set; }

    }
}
