using EmployeeSystemMangement.DAL.Entities;
using EmployeeSystemMangement.PL.Helpers;
using EmployeeSystemMangement.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.PL.Controllers
{
    [Authorize(Roles = "Super Admin,Admin")]

    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IWebHostEnvironment _env;

        public UserController(UserManager<ApplicationUsers> userManager, IWebHostEnvironment env)
        {
            _userManager=userManager;
            _env=env;
           

        }
        public async Task< IActionResult> Index(string name)
        {
            IEnumerable<ApplicationUsers> users;
            if (string.IsNullOrEmpty(name))
            {
                users =  (IEnumerable<ApplicationUsers>)await _userManager.Users.ToListAsync();
            }
            else
            {
                users = _userManager.Users.Where(u => u.NormalizedUserName.Contains(name.ToUpper()));
            }
                 return View(users);
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                return NotFound();
            }
            return View(viewName, user);
        }
        public async Task <IActionResult> Edit(string id, string viewName)
        {
            return await Details(id, viewName);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUsers user)
        {
            if (id != user.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(user);

            try
            {
                var oldUser = await _userManager.FindByIdAsync(id);
                if (oldUser == null)
                    return NotFound();

                // Update the existing user's properties
                oldUser.UserName = user.UserName;
                oldUser.NormalizedUserName = user.UserName.ToUpper();

                var result = await _userManager.UpdateAsync(oldUser);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");

                // Log the exception details here

                return View(user);
            }
        }

        public async Task< IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task <IActionResult> Delete(string id,ApplicationUsers applicationuser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var result=await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured");
                return View(applicationuser);

            }

        }



    }
}
