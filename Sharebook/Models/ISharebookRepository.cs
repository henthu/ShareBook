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
        City GetCityById(string id);
        bool SaveAll();
        Book GetBook(int id);
        void deleteBook(int id);
        ICollection<Comment> getBookComments(int id);
        Book GetBookWithComments(int bookId);
        string getBookOwner(int id);
        Comment getComment(int id);
        
        void deleteMessage(int id);
        ICollection<Message> getMessages(ApplicationUser reciever, ApplicationUser sender);
        ICollection<ApplicationUser> getCorrespondants(ApplicationUser currentUser);
        void AddMessage(Message newMessage);
        ICollection<Message> getRecievedMessages(ApplicationUser currentUser);
        DateTime getLastTalked(ApplicationUser currentUser, ApplicationUser correpondant);
        bool AreAllConversationsRead(ApplicationUser currentUser, ApplicationUser correpondant);
    }
}