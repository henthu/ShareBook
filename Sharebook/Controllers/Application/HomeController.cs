using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;
using Sharebook.ViewModels;
using Microsoft.AspNet.Identity;
using AutoMapper;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers.Application
{
    public class HomeController : Controller
    {
        private ISharebookRepository _repository;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ISharebookRepository repository,UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;

            
        }

       
        // GET: /<controller>/
        public IActionResult Index()
        {
           
            return View();
        }

        
    }
}
