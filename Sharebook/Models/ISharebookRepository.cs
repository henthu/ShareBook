using System;
using System.Collections.Generic;

namespace Sharebook.Models
{
    public interface ISharebookRepository
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetUserBooks(String userName);
        ApplicationUser GetUserByName(string userName);
        IEnumerable<City> GetCities(string CountryCode);

    }
}