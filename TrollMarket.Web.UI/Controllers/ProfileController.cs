using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.DTO.Profile;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.UI.Controllers
{
    [Authorize(Roles = "Buyer,Seller")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
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
            var topUp = new TopUpDTO();
            return View(dto);
        }

        [HttpPost]
        public IActionResult TopUp([FromBody] TopUpDTO dto) {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims;
            var username = claims?.SingleOrDefault(clm => clm.Type == "username")?.Value;

            if (ModelState.IsValid) { 
                            
                _service.TopUp(dto,username);
                return Ok("Top Up Succcessful");
            }
            return StatusCode(422, ModelState.Values.FirstOrDefault()?.Errors?.FirstOrDefault()?.ErrorMessage);
        }
    }
}
