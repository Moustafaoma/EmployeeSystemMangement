using EmployeeSystemMangement.DAL.Entities;
using EmployeeSystemMangement.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.PL.Controllers
{
	[Authorize]
	public class RolesController : Controller
	{
		private readonly RoleManager<ApplicationRoles> _roleManager;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IWebHostEnvironment _env;


        public RolesController(RoleManager<ApplicationRoles> roleManager,IWebHostEnvironment env, UserManager<ApplicationUsers> userManager
            )
		{
			_roleManager = roleManager;
			_env = env;
            _userManager = userManager;

		}

		public async Task<IActionResult> Index()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			return View(roles);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ApplicationRoles role)
		{
			if(ModelState.IsValid)
			{
				try
				{
					var result= await _roleManager.CreateAsync(role);
					if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
					
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        ModelState.AddModelError(string.Empty, "Failed To Added Employee");

                }
            }
			return View(role);
		}

		public async Task<IActionResult> Details(string id,string viewName="Details")
		{
            if (id is null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
           
            if (role is null)
                return NotFound();
            return View(viewName, role);
        }
        public async Task<IActionResult> Edit(string id, string viewName)
        {
            return await Details(id, viewName);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationRoles role)
        {
            if (id != role.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(role);

            try
            {
                var oldRole = await _roleManager.FindByIdAsync(id);
                if (oldRole == null)
                    return NotFound();

                // Update the existing user's properties
                oldRole.Name = role.Name;
                oldRole.NormalizedName = role.Name.ToUpper();

                var result = await _roleManager.UpdateAsync(oldRole);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(role);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");

                // Log the exception details here

                return View(role);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, ApplicationRoles applicationRole)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(role);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured");
                return View(applicationRole);

            }

        }
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return BadRequest();
            ViewBag.roleId = roleId;
            TempData["RoleName"] = role.Name;
            var usersInRole = new List<UsersInRoleViewModel>();
            var users=await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userInRole = new UsersInRoleViewModel()
                {
                    UserName=user.UserName,
                    UserId=user.Id,
                };
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                    userInRole.IsSelected = false;

                usersInRole.Add(userInRole);
            }
            return View(usersInRole);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(List<UsersInRoleViewModel> model, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("Role does not exist");
            }
            foreach (var user in model)
            {
                    var selectedUser = await _userManager.FindByIdAsync(user.UserId);
                if (user.IsSelected)
                {

                    if (!await _userManager.IsInRoleAsync(selectedUser, roleName))
                    {
                        await _userManager.AddToRoleAsync(selectedUser, roleName);
                    }
                }
                else
                {
                    //selectedUser = await _userManager.FindByIdAsync(user.UserId);

                    if (await _userManager.IsInRoleAsync(selectedUser, roleName))
                    {
                        await _userManager.RemoveFromRoleAsync(selectedUser, roleName);
                    }
                }
                    
            }
            return RedirectToAction("Index", "Roles"); // Adjust the redirection as needed

        }

    }
}
