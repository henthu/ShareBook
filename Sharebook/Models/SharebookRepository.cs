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
        

        public ApplicationUser GetUserByName(string userName)
        {
            return _context.Users
                .Where(user => user.UserName.ToLower() == userName.ToLower())
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
        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books;
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



        public ICollection<RecievedMessage> getMessages(ApplicationUser reciever,ApplicationUser sender)
        {
            return _context.Users
                .Where(user => user == reciever)
                .Include(user => user.RecievedMessages)
                .FirstOrDefault()
                ?.RecievedMessages
                ?.Where(r => r.Sender.UserName == sender.UserName)
                .ToList();
        }

        public void AddMessage(Message newMessage,ApplicationUser sender, ApplicationUser reciever)
        {
            SentMessage newSent = new SentMessage();
            newSent.Content = newMessage.Content;
            newSent.SendDate = newMessage.SendDate;
            newSent.Reciever = reciever;

            var senderUser = _context.Users
                .Where(user => user.UserName == sender.UserName)
                .Include(user => user.SentMessages)
                .FirstOrDefault();

            senderUser.SentMessages.Add(newSent);


            RecievedMessage newRecieved = new RecievedMessage();
            newRecieved.Content = newMessage.Content;
            newRecieved.SendDate = newMessage.SendDate;
            newRecieved.Sender = sender;
            newRecieved.isRead = false;

            var recieverUser = _context.Users
                .Where(user => user.UserName == reciever.UserName)
                .Include(user => user.RecievedMessages)
                .FirstOrDefault();
            recieverUser.RecievedMessages.Add(newRecieved);
        }

        public ICollection<RecievedMessage> getRecievedMessages(ApplicationUser currentUser)
        {
            return 
                _context.Users
                .Where(user => user == currentUser)
                .Include(user => user.RecievedMessages)
                .FirstOrDefault()
                ?.RecievedMessages.ToList();
        }

        public ICollection<ApplicationUser> getCorrespondants(ApplicationUser currentUser)
        {
            var senders = _context.Users
                .Where(user =>user == currentUser)
                .Include(user => user.RecievedMessages)
                .FirstOrDefault()
                ?.RecievedMessages
                ?.Select(r => r.Sender)
                ?.Distinct();
            var recievers = _context.Users
                .Where(user => user == currentUser)
                .Include(user => user.SentMessages)
                .FirstOrDefault()
                ?.SentMessages
                ?.Select(s => s.Reciever)
                ?.Distinct();

            return senders == null ? recievers.ToList() : senders.Concat(recievers)?.Distinct().ToList();
        }




        public DateTime? getLastTalked(ApplicationUser currentUser, ApplicationUser correpondant)
        {
            DateTime? lastSent = _context.Users
                .Where(user => user == currentUser)
                .Include(user => user.SentMessages)
                .FirstOrDefault()
                ?.SentMessages
                .Where(s=>s.Reciever.UserName == correpondant.UserName)
                ?.Select(s => s.SendDate).Max();
            DateTime? lastRecieved = _context.Users
                .Where(user => user == currentUser)
                .Include(user => user.RecievedMessages)
                .FirstOrDefault()
                ?.RecievedMessages
                .Where(r=>r.Sender.UserName == correpondant.UserName)
                ?.Select(s => s.SendDate).Max();

            return lastSent == null ? lastRecieved :
                            lastRecieved == null ? lastSent :
                                lastSent > lastRecieved ? lastSent : lastRecieved;

        }

        public bool AreAllConversationsRead(ApplicationUser currentUser, ApplicationUser correpondant)
        {

            return _context.Users
                .Where(user => user == currentUser)
                .Include(user => user.RecievedMessages)
                .FirstOrDefault()?.RecievedMessages == null 
                ?
                true 
                :
                _context.Users
                .Where(user => user == currentUser)
                .Include(user => user.RecievedMessages)
                .FirstOrDefault()
                .RecievedMessages
                .Where(r => r.Sender.UserName == correpondant.UserName)
                .Any(r => r.isRead == false);
        }
    }
}
