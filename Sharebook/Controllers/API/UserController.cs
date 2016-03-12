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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers.API
{
    [Route("/api/users")]
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
        [HttpGet]
        public JsonResult Get()
        {
           
            return Json(null);
        }

        // GET api/users/userName
        [HttpGet("{userName}")]
        public JsonResult Get(string userName)
        {
            return Json(null);
        }


        // DELETE api/users/userName
        [HttpDelete("{userName}")]
        public void Delete(string userName)
        {
        }
    }
}
