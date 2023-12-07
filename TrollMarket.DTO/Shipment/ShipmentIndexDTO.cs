using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Shipment
{
    public class ShipmentIndexDTO
    {
        public IEnumerable<ShipmentRowDTO> Grid { get; set; }

        public int TotalPages { get; set; }
    }
}
