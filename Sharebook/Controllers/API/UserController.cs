using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers.API
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private ISharebookRepository _context;

        public UserController(ISharebookRepository context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            var users = _context.GetAllUsers();
            return Json(null);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
