using authTest.Dto;
using authTest.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace authTest.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccoutService _accoutService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccoutService accoutService,ILogger<AccountController> logger)
        {
            _accoutService = accoutService;
            _logger = logger;
        }

        
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var res = await _accoutService.CheckLogin(dto);
            if (res != null)
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(res.Id)),
                    new Claim(ClaimTypes.Email, Convert.ToString(res.Email)),
                    new Claim(ClaimTypes.Role, res.RoleName??"admin")
                },
                CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToActionPermanent("Index", "Home");
                else
                    return Redirect(returnUrl);
            }
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
