using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Shop
{
    public class AddToCartDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public int ShipmentId { get; set; }
    }
}
