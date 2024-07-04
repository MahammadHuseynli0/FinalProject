using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NanoNexus.Core.Models;
using NanoNexus.ViewModels;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser
        //    {
        //        Name = "Mehemmed",
        //        Surname = "Huseynli",
        //        UserName = "Admin"

        //    };

        //    await _userManager.CreateAsync(user, "Admin123@");
        //    await _userManager.AddToRoleAsync(user, "Super Admin");

        //    return Ok("Admin yarandi");
        //}

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole("Super Admin");
        //    IdentityRole role1 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(role);
        //    await _roleManager.CreateAsync(role1);

        //    return Ok("Rollar yarandi");
        //}

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser user = await _userManager.FindByNameAsync(adminLoginVm.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is not valid");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, adminLoginVm.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is not valid");
                return View();
            }

            return RedirectToAction("Index", "dashboard");
        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

    }
}
