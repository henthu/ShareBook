using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sharebook.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Book> Books { get; set; }
    }
}