using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Shipment;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Provider.Implementation
{
    public  class ShipmentService : BaseService ,IShipmentService
    {
        public ShipmentIndexDTO GetIndex(int page)
        {
            var dto = new ShipmentIndexDTO();
          
            using (var dbContext = new TrollmarketContext())
            {
                var query = from ship in dbContext.Shippers
                            select new ShipmentRowDTO
                            {
                                Id = ship.Id,
                                Name = ship.Name,
                                Price = ToRupiah(ship.Price),
                                Service = ToYesNo(ship.Service)
                            };
                dto.TotalPages = GetTotalPage(query.Count());
                query = query.Skip(GetSkip(page)).Take(_totalPage);
                dto.Grid = query.ToList();
            }
            return dto;
        }
    }
}
