using Demo.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string SearchValue = "")
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var users = _userManager.Users;
                return View(users);
            }
            else
            {
                var users = await _userManager.Users.Where(x => x.NormalizedEmail.Contains(SearchValue.ToUpper())).ToListAsync();
                return View(users);
            }
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            return View(viewName, user);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationUser user)
        {
            if (id != user.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var appUser = await _userManager.FindByIdAsync(id);

                    appUser.UserName = user.UserName;
                    appUser.NormalizedUserName = user.UserName.ToUpper();
                    appUser.PhoneNumber = user.PhoneNumber;

                    var result = await _userManager.UpdateAsync(appUser);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            return View(user);
        }
        public async Task<IActionResult> Delete(string id, ApplicationUser user)
        {
            if (id != user.Id)
                return BadRequest();

            try
            {
                var appUser = await _userManager.FindByIdAsync(id);

                var result = await _userManager.DeleteAsync(appUser);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                ViewBag.Errors = result.Errors;
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
