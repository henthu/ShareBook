using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;
using AutoMapper;
using Sharebook.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers
{
    [Route("/api/users/{userName}/Books")]
    public class BookController : Controller
    {
        private ISharebookRepository _repository;

        public BookController(ISharebookRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        // GET: /<controller>/
        public JsonResult Get(string userName)
        {
            ApplicationUser userWithBooks =  _repository.GetUserBooks(userName);
            return Json(Mapper.Map<UserBooksViewModel>(userWithBooks));
        }
    }
}
