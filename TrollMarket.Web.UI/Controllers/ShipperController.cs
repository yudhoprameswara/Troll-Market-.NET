using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.UI.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class ShipperController : Controller
    {
        private readonly IShipmentService _service;

        public ShipperController(IShipmentService service)
        {
            _service = service;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            var dto = _service.GetIndex(page);
            return View(dto);
        }
    }
}
