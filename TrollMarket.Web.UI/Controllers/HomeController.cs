using Microsoft.AspNetCore.Mvc;

namespace TrollMarket.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
