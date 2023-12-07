using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.DTO.Shop;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Web.API.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _service;
        public ShopController(IShopService service)
        {
            _service = service;
        }


    }
}
