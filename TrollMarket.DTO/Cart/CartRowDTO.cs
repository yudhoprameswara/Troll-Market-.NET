using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Cart
{
    public class CartRowDTO
    {
        public int Id { get; set; }
        public string Product { get; set; }

        public int Quantity { get; set; }

        public string Shipment { get; set; }

        public string Seller { get; set; }

        public string TotalPrice { get; set; }
    }
}
