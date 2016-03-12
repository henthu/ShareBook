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


        // PUT api/users/
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = Mapper.Map<ApplicationUser>(user);
                    var result = await _userManager.CreateAsync(newUser, user.Password);
                    if(result.Succeeded){
                        await _signInManager.SignInAsync(newUser,false);
                        return Json(Mapper.Map<UserViewModel>(newUser));
                    }else
                    {
                         Response.StatusCode = (int)HttpStatusCode.BadRequest;
                         AddErrors(result);
                         return Json("Failed to create user"+result);
                    }
                    
                }
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to create user"+e.Message);
            }
            
              Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to create user");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        // DELETE api/users/userName
        [HttpDelete("{userName}")]
        public void Delete(string userName)
        {
        }
    }
}
