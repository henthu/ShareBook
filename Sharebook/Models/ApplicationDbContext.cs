using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Sharebook.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(){
            Database.EnsureCreated();
        }
        
        public DbSet<Book> Books { get; set; }
    }
}
