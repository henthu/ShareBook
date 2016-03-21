using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;
using Sharebook.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers.API
{
    [Route("/api/users")]
    [Authorize]
    public class UserController : Controller
    {
        private ISharebookRepository _repository;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserController(ISharebookRepository repository,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signinManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signinManager;
        }
        // GET: api/users
        [HttpGet("{userName}")]
         public JsonResult GetUserBooks(string userName)
        {
            ApplicationUser userWithBooks =  _repository.GetUserBooks(userName);
            return Json(Mapper.Map<UserBooksViewModel>(userWithBooks));
        }

        [HttpGet("")]
        public JsonResult GetMyBooks()
        {
            try {
                ApplicationUser userWithBooks = _repository.GetUserBooks(User.Identity.Name);
                return Json(Mapper.Map<UserViewModel>(userWithBooks));
            } catch(Exception e)
            {

            }
            return null;
        }



    }
}
