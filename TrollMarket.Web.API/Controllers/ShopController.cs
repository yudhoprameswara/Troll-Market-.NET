using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _service;
        public ShopController(IShopService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var dto = _service.GetDetails(id);
            return Ok(dto);
        }
    }
}
