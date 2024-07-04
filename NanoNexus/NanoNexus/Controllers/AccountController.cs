using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NanoNexus.Core.Models;
using NanoNexus.Data.DAL;
using NanoNexus.ViewModels;
using System.Threading.Tasks;

namespace NanoNexus.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<AppUser> userManager, AppDbContext appDbContext, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            AppUser user = await _userManager.FindByNameAsync(vm.Name);

            if (user != null)
            {
                ModelState.AddModelError("UserName", "Username already exists!");
                return View(vm);
            }

            user = await _userManager.FindByEmailAsync(vm.Email);

            if (user != null)
            {
                ModelState.AddModelError("Email", "Email already exists!");
                return View(vm);
            }

            user = new AppUser()
            {
                UserName = vm.Name,
                Email = vm.Email
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(vm);
            }

            if (!await _roleManager.RoleExistsAsync("Member"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Member"));
            }

            await _userManager.AddToRoleAsync(user, "Member");

            // Save changes to the database
            await _appDbContext.SaveChangesAsync();

            // Redirect to a different page after successful registration
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Profile()
        {
            AppUser appUser = null;
            

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            if (appUser == null) return RedirectToAction("Index", "Home");
            List<Order> orders = await _appDbContext.Orders
                                                    .Where(x => x.AppUserId == appUser.Id)
                                                    .ToListAsync();

            return View(orders);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            AppUser user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                _logger.LogWarning($"Login attempt failed for email: {vm.Email}. User not found.");
                ModelState.AddModelError("", "Email or Password is not valid!");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                _logger.LogWarning($"Login attempt failed for email: {vm.Email}. Sign-in result: {result.ToString()}");
                ModelState.AddModelError("", "Email or Password is not valid!");
                return View(vm);
            }

            _logger.LogInformation($"User with email: {vm.Email} logged in successfully.");

            // Redirect to a different page after successful login
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}