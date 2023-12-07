using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Account;
using TrollMarket.DTO.Admin;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.UI.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {

        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }
        public IActionResult NewAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AdminRegisterDTO dto)
        {
            if (ModelState.IsValid)
            {
                _service.RegisterAdmin(dto);
                return RedirectToAction("Login","Account");
            }
            return View("NewAdmin",dto);
        }
    }
}
