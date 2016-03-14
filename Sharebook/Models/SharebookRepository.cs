﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharebook.Models
{
    public class SharebookRepository : ISharebookRepository
    {
        private ApplicationDbContext _context;

        public SharebookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books;
        }

        public ApplicationUser GetUserByName(string userName)
        {
            return _context.Users
                .Where(user => user.UserName == userName)
                .FirstOrDefault();
        }
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _context.Users;
        }

        public IEnumerable<Book> GetUserBooks(string userName)
        {
            ApplicationUser user = GetUserByName(userName);

            return user?.Books;
        }

        public IEnumerable<City> GetCities(string countryCode)
        {
            IEnumerable<City> result = _context.Cities
                                        .Where(city => city.CountryCode == countryCode)
                                        .ToList();
            return result;
        }
        public City GetCityByName(string name){
            City city = _context.Cities
                    .Where(c=>c.Name == name)
                    .FirstOrDefault();
            return city;
        }
    }
}
