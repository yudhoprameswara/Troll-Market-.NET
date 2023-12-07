using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Utility;

namespace TrollMarket.DTO.Shop
{
    public class ShopIndexDTO
    {
        public IEnumerable<ShopRowDTO> Grid { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<DropdownDTO> ShipperDropdown { get; set; }
    }
}
