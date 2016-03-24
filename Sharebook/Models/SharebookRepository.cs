using Microsoft.Data.Entity;
using System;
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

        public ApplicationUser GetUserBooks(string userName)
        {
            var userWithBooks = _context.Users
                                .Include(user => user.Books)
                                .ThenInclude(book => book.Comments)
                                .Where(user => user.UserName == userName)
                                .FirstOrDefault();

            return userWithBooks;
        }

        public IEnumerable<City> GetCities(string countryCode)
        {
            IEnumerable<City> result = _context.Cities
                                        .Where(city => city.CountryCode == countryCode)
                                        .OrderBy(city => city.Name)
                                        .ToList();
            return result;
        }
        public City GetCityById(string id){
           
            int intId;
            City city;
            
            if(int.TryParse(id,out intId)){
                city = _context.Cities
                .Where(c=>c.Id == intId)
                .FirstOrDefault();
            }else
            {
                city = null;
            }
            
            return city;
            
        }
        
          public Book GetBook(int bookId)
        {
            return _context.Books.Where(bk=>bk.Id == bookId).FirstOrDefault();
        }
          public Book GetBookWithComments(int bookId)
        {
            var book =_context.Books.
                    Where(bk=>bk.Id == bookId)
                    .Include(bk =>bk.Comments)
                    .FirstOrDefault();
            book.Comments = book.Comments.OrderByDescending(comment => comment.CreatedAt).ToList();        
            
            return book;
                    
        }
        

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void deleteBook(int id)
        {
            _context.Books.Remove(GetBook(id));
            
        }

        public ICollection<Comment> getBookComments(int id)
        {
            return _context.Books.Where(book => book.Id == id)
                    .Include(x=>x.Comments)
                    .FirstOrDefault()
                    ?.Comments
                    ?.OrderByDescending(comment => comment.CreatedAt)
                    ?.ToList();
                             
        }
        public string getBookOwner(int id){
            string userName = _context.Users
                                .Where(user => user.Books.Any(book => book.Id == id))
                                .FirstOrDefault()
                                ?.UserName; 
            
            return userName;
        }

        public Comment getComment(int id)
        {
            return _context.Comments.Where(comment => comment.Id == id).FirstOrDefault();
        }
    }
}
