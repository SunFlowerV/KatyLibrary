using KatyLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminUsersController : Controller
    {
        UserManager<IdentityUser> _userManager;

        public AdminUsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        public IActionResult Index() => View(_userManager.Users.ToList());

        
        public IActionResult Create() => View();

        [HttpPost]
        
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleresult = await _userManager.AddToRoleAsync(user, "user");
                    if (roleresult.Succeeded)
                    {
                        return RedirectToAction("Index", "AdminUsers");
                    }
                    else
                    {
                        foreach (var error in roleresult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        
        public async Task<IActionResult> Edit(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "AdminUsers");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index", "AdminUsers");
        }

        
        public async Task<IActionResult> ChangePassword(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            AdminChangePasswordViewModel model = new AdminChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AdminChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<IdentityUser>)) as IPasswordValidator<IdentityUser>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<IdentityUser>)) as IPasswordHasher<IdentityUser>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index", "AdminUsers");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
    }
}
