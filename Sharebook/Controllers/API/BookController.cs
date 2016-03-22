using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;
using AutoMapper;
using Sharebook.ViewModels;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers
{
    [Route("/api/Books")]
    [Authorize]
    public class BookController : Controller
    {
        private ISharebookRepository _repository;

        public BookController(ISharebookRepository repository )
        {
            _repository = repository;
        }
        [HttpGet("")]
        public JsonResult GetMyBooks(){
            
            IEnumerable<Book> currentUserBooks  = _repository.GetUserBooks(User.Identity.Name)?.Books;
            
            return Json(Mapper.Map<IEnumerable<BookViewModel>>(currentUserBooks));
        }
        
        [HttpPost("")]
        public JsonResult AddNewBook([FromBody] BookViewModel newBook){
            
            ApplicationUser currentUser = _repository.GetUserBooks(User.Identity.Name);
            var book = Mapper.Map<Book>(newBook);
            currentUser?.Books.Add(book);
            
            if(_repository.SaveAll()){
                Response.StatusCode = (int) HttpStatusCode.Created;
                return Json(Mapper.Map<BookViewModel>(book));
            }else{
                Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                return Json(null);
            }
        }
        
         [HttpPost("{bookId}")]
        public JsonResult EditBook([FromBody] BookViewModel newBook){
            
            ApplicationUser currentUser = _repository.GetUserBooks(User.Identity.Name);
            var book = _repository.GetBook(Mapper.Map<Book>(newBook).Id);
            if(book != null){
                book.Author = newBook.Author;
                book.Name = newBook.Name;
                book.Comments = Mapper.Map<ICollection<Comment>>(newBook.Comments);
            }
            
            
            if(_repository.SaveAll()){
                Response.StatusCode = (int) HttpStatusCode.OK;
                return Json(Mapper.Map<BookViewModel>(book));
            }else{
                Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                return Json(null);
            }
        }
        
        [HttpDelete("{bookId}")]
        public JsonResult DeleteBook(string bookId){
            int id;
            if(int.TryParse(bookId, out id)){
                _repository.deleteBook(id);
                if(_repository.SaveAll()){
                    Response.StatusCode = (int) HttpStatusCode.Created;
                    return(Json(new {success = "true", errorMessage = ""}));
                }else{
                    Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    return(Json(new {success = "false", errorMessage = "book could not be removed from database"}));
                }
            }
            return(Json(new {success = "false", errorMessage = "bookId format not available : "+bookId}));
            
        }
        
        [HttpPost("{bookId}/comment")]
        public JsonResult AddComment(string bookId,[FromBody] CommentViewModel newComment){
            int id;
            ApplicationUser currentUser = _repository.GetUserBooks(User.Identity.Name);
            
            if(int.TryParse(bookId, out id)){
                Comment comment = new Comment();
                comment.Content = newComment.Content;
                comment.BookId = id;
                comment.CreatedAt = DateTime.Now;
                comment.UserName = currentUser.UserName;
                
                Book book = _repository.GetBook(id);
                if(currentUser.Comments == null)
                {
                    currentUser.Comments = new List<Comment>();
                }
                currentUser.Comments.Add(comment);
                if (book.Comments == null)
                {
                    book.Comments = new List<Comment>();
                }
                book.Comments.Add(comment);
                
                if(_repository.SaveAll()){
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return Json(Mapper.Map<CommentViewModel>(comment));
                }else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(null);
                }
                
            }
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(null);
        }
        [HttpGet("{bookId}")]
        public JsonResult GetBookWithComments(string bookId){
            int id;
            
            if(int.TryParse(bookId, out id)){
                Book book = _repository.GetBookWithComments(id);
                
                return (Json(Mapper.Map<BookViewModel>(book)));
            }
            
            return Json(null);
        }
        [HttpGet("{bookId}/comments")]
        public JsonResult GetComment(string bookId){
            int id;
            
            if(int.TryParse(bookId, out id)){
                Book book = _repository.GetBook(id);
                var comments = Mapper.Map<IEnumerable<CommentViewModel>>(_repository.getBookComments(id));
                return (Json(comments));
            }
            
            return Json(null);
        }
    }
}
