using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.DAL.Entities;
using EmployeeSystemMangement.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        public AccountController(UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult>Register(RegisterViewModel model)
		{
            if(ModelState.IsValid)
            {
                //Check if user name if exists
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user is  null)
                {
					user = new ApplicationUsers()
					{
						FirstName = model.FirstName,
						LastName = model.LastName,
						UserName = model.Username,
						Email = model.Email,
						IsAgree = model.IsAgree,
					};
                 var result=   await _userManager.CreateAsync(user,model.Password);
					if (result.Succeeded)
                    {
                        return RedirectToAction("LogIn");
                    }
                    foreach (var error in result.Errors)
                    {
						ModelState.AddModelError(string.Empty,error.Description );

					}
				}
                else
				ModelState.AddModelError(string.Empty, "The Username already exists. Please choose a different one.");

			}
			return View(model);
		}
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(model.Email);

				if (user is not null)
                {
                    var flag=await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
						
						var result = await _signInManager.PasswordSignInAsync(user,model.Password,model.IsRemember,false);
                        if (result.IsLockedOut)
                        {
							ModelState.AddModelError(string.Empty, "Your Account is locked");

						}

						if (result.Succeeded)
                        {
                            return RedirectToAction("Index","Home");
                        }

                    }
				}
				ModelState.AddModelError(string.Empty, "Invalid LogIn");
            }
            return View(model);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
		public async Task <IActionResult> SendResetPasswordEmail(ForgetPassordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByNameAsync(model.Email);
                if(user is not null)
                {
                    
                }
                ModelState.AddModelError(string.Empty, "This Account not found..");
            }
            return View(model);

        }

	}
}
