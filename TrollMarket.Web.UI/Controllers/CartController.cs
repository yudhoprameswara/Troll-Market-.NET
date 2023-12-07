using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.UI.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class CartController : Controller
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }
        public IActionResult Index(int page = 1)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims;
            var username = claims?.SingleOrDefault(clm => clm.Type == "username")?.Value;
            var dto = _service.GetIndex(page, username);
            ViewBag.Page = page;
            ViewBag.Error = true;
            return View(dto);
        }

        public IActionResult PurchaseAll(int page = 1) {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims;
            var username = claims?.SingleOrDefault(clm => clm.Type == "username")?.Value;
            ViewBag.Error = _service.PurchaseAll(username);
            ViewBag.Page = page;
            var dto = _service.GetIndex(page, username);
            return View("Index",dto);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int id)
        {
                _service.Delete(id);
                return RedirectToAction("Index");
        }

    }
}
