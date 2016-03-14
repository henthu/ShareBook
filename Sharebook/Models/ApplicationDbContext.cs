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
        public DbSet<City> Cities{get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:DefaultConnection:ConnectionString"];
            optionsBuilder.UseSqlite(connString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
