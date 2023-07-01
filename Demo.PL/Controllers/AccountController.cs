using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
           _signInManager = signInManager;
        }

        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    IsAgree = model.IsAgree
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("SignIn");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        #endregion

        #region SignIn
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null)
                    ModelState.AddModelError("", "Invalid Email");

                var password = await _userManager.CheckPasswordAsync(user, model.Password);

                if (password)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                        //if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        //    return RedirectToAction("Index", "Users");
                        //else if (_signInManager.IsSignedIn(User) && User.IsInRole("Doctor"))
                            return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
        #endregion

        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetPassordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = resetPassordLink,
                        To = model.Email
                    };

                    EmailSettings.SendEmail(email);

                    return RedirectToAction(nameof(CompleteForgetPassword));
                }
                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }
        #endregion

        #region CompleteForgetPassword
        public IActionResult CompleteForgetPassword()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(ResetPasswordDone));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        #endregion

        public IActionResult ResetPasswordDone()
        {
            return View();
        }
    }
}
