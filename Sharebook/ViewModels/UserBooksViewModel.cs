using Sharebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharebook.ViewModels
{
    class UserBooksViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
