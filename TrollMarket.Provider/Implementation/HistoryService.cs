using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.History;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Provider.Implementation
{
    public class HistoryService : BaseService, IHistoryService
    {
        public HistoryIndexDTO GetIndex(int page,string buyerId, string seller)
        {
            var dto = new HistoryIndexDTO {
                BuyerDropdown = GetBuyerDropdown(),
                SellerDropdown = GetSellerDropdown(),
            };
            var buyer = int.Parse(buyerId);
            var sellers = int.Parse(seller);

            using (var dbContext = new TrollmarketContext()) {
                IQueryable<Purchase> purchases = dbContext.Purchases;
                if (buyer > 0) { 
                    purchases = purchases.Where(pur => pur.BuyerId == buyer);
                }

                if (sellers > 0) {
                    purchases = purchases.Where(pur => pur.Product.SellerId == sellers);
                }

                var query = from pur in purchases
                            where pur.Date != null 
                            select new HistoryRowDTO
                            {
                                Date = ((DateTime)pur.Date).ToString("dd/MM/yyyy HH:mm:ss"),
                                Seller = pur.Product.Seller.Name,
                                Product = pur.Product.Name,
                                Quantity = (int)pur.Quantity,
                                Shipment = pur.Shipment.Name,
                                TotalPrice = ToRupiah((pur.Product.Price*pur.Quantity)+pur.Shipment.Price)
                            };
                dto.TotalPages = GetTotalPage(query.Count());
                query = query.Skip(GetSkip(page)).Take(_totalPage);
                dto.Grid = query.ToList();
            }
             return dto;
        }








    }
}
