using IdentityProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers
{
    
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> UserManager;
        private SignInManager<AppUser> SignInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public async Task<IActionResult> Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindByEmailAsync(login.Email);
                if (user!=null)
                {
                    await SignInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(login.ReturnUrl ?? "/");
                    }
                    ModelState.AddModelError(nameof(login.Email), "Login failed!Email or Password is invalid!");
                }
            }
            return View(login);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
