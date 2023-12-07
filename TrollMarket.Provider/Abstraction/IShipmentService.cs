using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Shipment;

namespace TrollMarket.Provider.Abstraction
{
    public interface IShipmentService
    {
        public ShipmentIndexDTO GetIndex(int page);
    }
}
