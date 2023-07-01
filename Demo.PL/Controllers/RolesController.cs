using Demo.DAL.Entities;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (System.Exception)
                {

                    throw;
                }
            }

            return View(role);
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return NotFound();

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();

            return View(viewName, role);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole role)
        {
            if (id != role.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var appRole = await _roleManager.FindByIdAsync(id);

                    appRole.Name = role.Name;
                    appRole.NormalizedName = role.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(appRole);

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

            return View(role);
        }

        public async Task<IActionResult> Delete(string id, ApplicationRole role)
        {
            if (id != role.Id)
                return BadRequest();

            try
            {
                var appRole = await _roleManager.FindByIdAsync(id);

                var result = await _roleManager.DeleteAsync(appRole);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                ViewBag.Errors = result.Errors;
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return BadRequest();

            ViewBag.RoleId = roleId;

            var users = new List<UserInRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                // update on checkbox  if role existed or not 
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                users.Add(userInRole);
            }

            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleViewModel> users, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                foreach (var item in users)
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);

                    if (user != null)
                    {
                        if (item.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                            await _userManager.AddToRoleAsync(user, role.Name);
                        else if (!item.IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                return RedirectToAction(nameof(Index), new { id = roleId });
            }

            return View(users);
        }
    }
}
