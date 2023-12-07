using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.DTO.Merchandise;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.UI.Controllers
{
    [Authorize(Roles = "Seller")]
    public class MerchandiseController : Controller
    {
        private readonly IMerchandiseService _service;

        public MerchandiseController(IMerchandiseService service)
        {
            _service = service;
        }
        public IActionResult Index(int page = 1)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims;
            var username = claims?.SingleOrDefault(clm => clm.Type == "username")?.Value;
            ViewBag.Page = page;
            var dto = _service.GetIndex(page, username);
            return View(dto);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var dto = _service.FindOne(id);
            return Ok(dto);
        }

        [HttpGet]
        public IActionResult UpsertForm(int id) {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims;
            var username = claims?.SingleOrDefault(clm => clm.Type == "username")?.Value;
            var dto = new UpsertProductDTO();

            var dependetProduct = _service.CountDependentProducts(id);
            if (dependetProduct == 0)
            {
                if (id > 0)
                {
                    dto = _service.FindOne(id);
                }
                ViewBag.SellerId = _service.GetSellerIdByUsername(username);
                return View(dto);
              
            }
            return RedirectToAction("EditFail", new { dependecies = dependetProduct });


        }

        [HttpPost]
        public IActionResult Upsert(UpsertProductDTO dto)
        {
            if (ModelState.IsValid)
            {
                _service.Save(dto);
                return RedirectToAction("Index");

            }
            return View("UpsertForm",dto);

        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int id)
        {
            var dependetProduct = _service.CountDependentProducts(id);
            if (dependetProduct == 0)
            {
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditFail", new { dependecies = dependetProduct });

        }

        [HttpGet]
        public IActionResult EditFail(int dependecies)
        {
            return View(dependecies);
        }

        [HttpGet]
        public IActionResult Discontinue(int id) {
            _service.Discontinue(id);
            return RedirectToAction("Index");
        }
    }
}

