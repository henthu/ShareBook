using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sharebook.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; }
        public City City  { get; set; }
        public string Country { get; set; }
    }
}