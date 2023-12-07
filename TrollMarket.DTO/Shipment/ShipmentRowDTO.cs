using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Shipment
{
    public class ShipmentRowDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Price { get; set; }

        public string Service { get; set; }
    }
}
