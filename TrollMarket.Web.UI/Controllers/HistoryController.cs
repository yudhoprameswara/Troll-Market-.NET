using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.DataAccess.Entity;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _service;

        public HistoryController(IHistoryService service)
        {
            _service = service;
        }
        public IActionResult Index(int page = 1,string buyerId = "0",string seller = "0")
        {
            buyerId ??= "0";
            seller??= "0";
            var dto = _service.GetIndex(page,buyerId,seller);
            ViewBag.Page = page;
            ViewBag.BuyerId = buyerId;
            ViewBag.Seller = seller;
            return View(dto);
        }
    }
}
