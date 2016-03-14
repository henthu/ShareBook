using System;
using System.Collections.Generic;

namespace Sharebook.Models
{
    public interface ISharebookRepository
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        IEnumerable<Book> GetAllBooks();
        ApplicationUser GetUserBooks(String userName);
        ApplicationUser GetUserByName(string userName);
        IEnumerable<City> GetCities(string CountryCode);
        City GetCityByName(string name);
    }
}