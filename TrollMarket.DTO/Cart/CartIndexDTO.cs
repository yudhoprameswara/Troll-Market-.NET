using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Cart
{
    public class CartIndexDTO
    {
        public IEnumerable<CartRowDTO> Grid { get; set; }

        public int TotalPages { get; set; }
    }
}
