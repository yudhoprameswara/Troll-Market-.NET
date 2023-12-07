using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.DTO.Shop;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.UI.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Authorize(Roles = "Buyer")]
    public class ShopController : Controller
    {

        private readonly IShopService _service;

        public ShopController(IShopService service)
        {
            _service = service;
        }
        public IActionResult Index(int page = 1, string name = "", string category = "")
        {
            name ??= "";
            category ??= "";
            var dto = _service.GetIndex(page, name, category);
            ViewBag.Page = page;
            ViewBag.Name = name;
            ViewBag.Category = category;
            return View(dto);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var dto = _service.GetDetails(id);
            return Ok(dto);
        }


        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCartDTO dto)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims;
            var username = claims?.SingleOrDefault(clm => clm.Type == "username")?.Value;
            _service.AddToCart(username, dto);
            return Ok("Add To Cart Successful");
        }

   
    }
}
