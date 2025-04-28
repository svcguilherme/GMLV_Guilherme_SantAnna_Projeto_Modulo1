using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GLMV.AppWeb.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly SalesPersonService _salesPersonService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, SalesPersonService salesPersonService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _salesPersonService = salesPersonService;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any())
                    HttpContext.Session.SetString("UserRole", roles.First());

                HttpContext.Session.SetString("UserId", user.Id);
                HttpContext.Session.SetString("UserEmail", user.Email);

                var salesPerson = _salesPersonService.GetByIdString(user.Id);
                if (salesPerson != null)
                {
                    HttpContext.Session.SetString("Name", $"{salesPerson.FirstName} {salesPerson.LastName}");
                }

                return RedirectToAction("Index", "GestaoLoja", new { id = user.Id });
            }

            if (result.IsLockedOut)
            {
                return View("Lockout");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet("registrar")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("registrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "SalesPerson");

                var salesPerson = new SalesPerson
                {
                    Id = user.Id,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                await _salesPersonService.AddAsync(salesPerson);
                await _salesPersonService.SaveASync();

                await _signInManager.SignInAsync(user, isPersistent: false);

                HttpContext.Session.SetString("UserId", user.Id);
                HttpContext.Session.SetString("Name", $"{model.FirstName} {model.LastName}");

                return RedirectToAction("Index", "GestaoLoja", new { id = user.Id });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
