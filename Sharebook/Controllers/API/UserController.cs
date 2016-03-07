using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;
using Sharebook.ViewModels;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers.API
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private ISharebookRepository _repository;

        public UserController(ISharebookRepository repository)
        {
            _repository = repository;
        }
        // GET: api/users
        [HttpGet]
        public JsonResult Get()
        {
            var users = _repository.GetAllUsers();
            var usersVm = Mapper.Map<IEnumerable<UserViewModel>>(users);
            
            return Json(usersVm);
        }

        // GET api/users/userName
        [HttpGet("{userName}")]
        public JsonResult Get(string userName)
        {
            return Json(Mapper.Map<UserViewModel>(_repository.GetUserByName(userName)));
        }


        // PUT api/users/userName
        [HttpPut("{userName}")]
        public void Put(string userName, [FromBody]string value)
        {
            
        }

        // DELETE api/users/userName
        [HttpDelete("{userName}")]
        public void Delete(string userName)
        {
        }
    }
}
