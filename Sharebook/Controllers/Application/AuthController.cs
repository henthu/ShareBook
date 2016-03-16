using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Sharebook.Models;
using Sharebook.ViewModels;
using AutoMapper;
using System.Linq;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers.Application
{
    public class AuthController : Controller
    {
        private SignInManager<ApplicationUser> _signinManager;
        private UserManager<ApplicationUser> _userManager;
        private ISharebookRepository _repository;
        private ApplicationDbContext _context;

        public AuthController(SignInManager<ApplicationUser> signinManager,
         UserManager<ApplicationUser> userManager,
         ISharebookRepository repository,
         ApplicationDbContext context)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _repository = repository;
            _context = context;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(vm.UserName, vm.Password, true, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password incorrect");
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = Mapper.Map<ApplicationUser>(vm);
                
                newUser.City = _repository.GetCityById(vm.City);
                var result = await _userManager.CreateAsync(newUser, vm.Password);
                if (result.Succeeded)
                {
                    await _signinManager.SignInAsync(newUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ApplicationUserValidator validator = new ApplicationUserValidator(_context);
                    var results = validator.Validate(newUser);
                    if(!results.IsValid){
                        ModelState.AddModelError("","username already used, choose another one");
                    }else{
                        ModelState.AddModelError("", "could not register user");
                    }
                }

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signinManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult ChangePassword(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    string curentUserID = User.GetUserId();
                    ApplicationUser currentUser = _context.Users
                            .Where(user => user.Id == curentUserID)
                            .FirstOrDefault();
                                                  
                    var result = await _userManager.ChangePasswordAsync(currentUser, vm.OldPassword, vm.NewPassword);
                    if(result.Succeeded){
                        return RedirectToAction("Index","Home");
                    }else{
                        ModelState.AddModelError("", "Something went wrong, could not change password");
                    }
                }
            }
            return View();
        }

    }
}