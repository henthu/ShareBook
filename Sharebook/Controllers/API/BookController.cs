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
        public JsonResult Get(){
            
            var currentUserBooks  = _repository.GetUserBooks(User.Identity.Name);
            
            return Json(Mapper.Map<UserBooksViewModel>(currentUserBooks));
        }
        
        [HttpPost("")]
        public JsonResult Post([FromBody] BookViewModel newBook){
            
            ApplicationUser currentUser = _repository.GetUserBooks(User.Identity.Name);
            var book = Mapper.Map<Book>(newBook);
            currentUser?.Books.Add(book);
            if(_repository.SaveAll()){
                Response.StatusCode = (int) HttpStatusCode.Created;
                return Json(Mapper.Map<BookViewModel>(book));
            }
            return Json(null);
        }
    }
}
