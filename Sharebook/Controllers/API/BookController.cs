using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers
{
    [Route("/api/users/{userName}/Books")]
    public class BookController : Controller
    {
        private ISharebookRepository _context;

        public BookController(ISharebookRepository context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
