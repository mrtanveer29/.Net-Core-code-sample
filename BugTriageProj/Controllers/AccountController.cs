using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using BugTriage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTriage.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
           
            return View();
        }
        [AllowAnonymous]
        public ViewResult Login() {
            return View();
        }
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user, string returnUrl) {
            if (ModelState.IsValid) {
                var result = await signInManager.PasswordSignInAsync(user.UserName, user.Password,user.RememberMe, false);
                if (result.Succeeded) {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Developer");
               
                }
            }
            ModelState.AddModelError(String.Empty,"Login Unsuccessful"); 
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid) {
                IdentityUser identityUser = new IdentityUser { UserName = user.UserName, Email = user.Email , PhoneNumber=user.PhoneNumber};
                var result= await userManager.CreateAsync(identityUser, user.Password);

                if (result.Succeeded) {
                    await signInManager.SignInAsync(identityUser, isPersistent:false);
                    return RedirectToAction("Index","Developer");
                }
                foreach (IdentityError error in result.Errors) {
                    ModelState.AddModelError("key",error.Description);
                }
            }
            return View(user);
        }
    }
}