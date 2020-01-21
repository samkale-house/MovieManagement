using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {            
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
           //validate model
           //create user useriden
           //if passed signin 
           if(ModelState.IsValid)
            {
                var newUser = new IdentityUser() { UserName = registerVM.Email, Email = registerVM.Email };
                var result = await _userManager.CreateAsync(newUser, registerVM.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index","home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM,string returnurl)
        {
            //persistent cookie:if rembmerme is checked, identity cookie is persisted even if browser window closed and open again,user will be loggedin
            //Session cookieL if remember me is not selected,login cookie will be sent to server throught the server-client cmmunication to keep logged in throught session,but identity cookie
            //is lost when browser window closed and reopend ,user will be logged out.
            //returnurl present in Querystring,used to retrun to requested page after login
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, loginVM.RememberMe, false);
                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnurl) && Url.IsLocalUrl(returnurl))  //prevent open redirect attack chevk for local\external url
                    {
                        return Redirect(returnurl);
                        //return LocalRedirect(returnurl);
                    }
                    return RedirectToAction("Index", "home");
                }


                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
               
            }
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
