using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;
using Sharebook.ViewModels;
using AutoMapper;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers.Application
{
    public class AuthController : Controller
    {
        private SignInManager<ApplicationUser> _signinManager;
        private UserManager<ApplicationUser> _userManager;

        public AuthController(SignInManager<ApplicationUser> signinManager,UserManager<ApplicationUser> userManager){
            _signinManager = signinManager;
            _userManager = userManager;
        }
        public IActionResult Login(){
            if(User.Identity.IsAuthenticated){
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm,string returnUrl){
            if(ModelState.IsValid){
                var result = await _signinManager.PasswordSignInAsync(vm.UserName,vm.Password,true,false);
                if(result.Succeeded){
                    if(string.IsNullOrWhiteSpace(returnUrl)){
                        return RedirectToAction("Index","Home");
                    }else{
                        return Redirect(returnUrl);
                    }
                }else
                {
                    ModelState.AddModelError("","UserName or Password incorrect");
                }
            }
            return View();
        }
        
        public IActionResult Register(){
            return View();
        }
        
        [HttpPost]
         public async Task<IActionResult> Register(RegisterViewModel vm){
            if (ModelState.IsValid)
            {
                var newUser = Mapper.Map<ApplicationUser>(vm);
                var result = await _userManager.CreateAsync(newUser, vm.Password);
                if(result.Succeeded){
                    await _signinManager.SignInAsync(newUser,false);
                    return RedirectToAction("Index","Home");
                }else
                {
                        ModelState.AddModelError("","Something went wrong, could not register user");
                }
                
            }
            return View();
        }
        
    }
}