using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.DTO.Account;
using TrollMarket.DTO.Utility;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl = "/")
        {
            ViewBag.ReturnUrl = returnUrl;
            var dto = new LoginDTO();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                var isAuthenticated = _service.IsAuthenticated(dto);
                if (isAuthenticated)
                {
                    var claimsPrincipal = GetClaims(dto);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("/");
                }
            }
            return View(dto);
        }

        private ClaimsPrincipal GetClaims(LoginDTO dto)
        {
            var role = _service.GetRole(dto.Username);
            var claims = new List<Claim>
            {
                new Claim("username",dto.Username),
                new Claim(ClaimTypes.Role,role),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);

            return principle;
        }

        [HttpGet]
        public IActionResult RegisterBuyer()
        {
            var dto = new RegisterDTO();
            dto.Role = Role.Buyer.ToString();
            dto.Balance = 0;
            return View("Register",dto);
        }

        [HttpGet]
        public IActionResult RegisterSeller()
        {
            var dto = new RegisterDTO();
            dto.Role = Role.Seller.ToString();
            dto.Balance = 0;
            return View("Register",dto);
        }


        [HttpPost]
        public IActionResult Register(RegisterDTO dto)
        {
            if (ModelState.IsValid)
            {
                _service.Register(dto);
                return RedirectToAction("Login");
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
