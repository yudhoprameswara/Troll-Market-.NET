using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Profile;
using TrollMarket.DTO.Utility;
using TrollMarket.Provider.Abstraction;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrollMarket.Provider.Implementation
{
    public class ProfileService : BaseService, IProfileService
    {
        public ProfileIndexDTO GetIndex(int page, string username)
        {
            var indexDto = new ProfileIndexDTO();
            string role = GetRoleByUsername(username);
            var detailDto = GetDetails(username, role);
            indexDto.DetailInfo = detailDto;
            var id = detailDto.Id;
            if (role == Role.Seller.ToString())
            {
                using (var dbContext = new TrollmarketContext())
                {
                    var query = from pur in dbContext.Purchases
                                where pur.Product.SellerId == id && pur.Date != null
                                select new ProfileTransactionRowDTO
                                {
                                    Date = ((DateTime)pur.Date!).ToString("dd/MM/yyyy HH:mm:ss"),
                                    Product = pur.Product.Name,
                                    Quantity = (int)pur.Quantity,
                                    Shipment = pur.Shipment.Name,
                                    TotalPrice = ToRupiah(pur.Product.Price * pur.Quantity)
                                };
                    indexDto.TotalPages = GetTotalPage(query.Count());
                    query = query.Skip(GetSkip(page)).Take(_totalPage);
                    indexDto.Grid = query.ToList();
                }

            }
            else if (role == Role.Buyer.ToString())
            {
                using (var dbContext = new TrollmarketContext())
                {
                    var query = from pur in dbContext.Purchases
                                where pur.BuyerId == id && pur.Date != null
                                select new ProfileTransactionRowDTO
                                {
                                    Date = ((DateTime)pur.Date!).ToString("dd/MM/yyyy HH:mm:ss"),
                                    Product = pur.Product.Name,
                                    Quantity = (int)pur.Quantity,
                                    Shipment = pur.Shipment.Name,
                                    TotalPrice = ToRupiah((pur.Product.Price * pur.Quantity) + pur.Shipment.Price)
                                };
                    indexDto.TotalPages = GetTotalPage(query.Count());
                    query = query.Skip(GetSkip(page)).Take(_totalPage);
                    indexDto.Grid = query.ToList();
                }
            }

            return indexDto;
        }


        public ProfileDetailDTO GetDetails(string username, string role) {
            var detailDto = new ProfileDetailDTO();
            if (role == Role.Seller.ToString())
            {
                using (var dbContext = new TrollmarketContext())
                {
                    var seller = dbContext.Sellers.SingleOrDefault(buy => buy.Username == username);
                    detailDto.Id = seller.Id;
                    detailDto.Name = seller.Name;
                    detailDto.Address = seller.Address;
                    detailDto.Balance = ToRupiah(seller.Balance);
                    detailDto.Role = role;

                }
            }
            else if (role == Role.Buyer.ToString())
            {
                using (var dbContext = new TrollmarketContext())
                {
                    var buyer = dbContext.Buyers.SingleOrDefault(buy => buy.Username == username);
                    detailDto.Id = buyer.Id;
                    detailDto.Name = buyer.Name;
                    detailDto.Address = buyer.Address;
                    detailDto.Balance = ToRupiah(buyer.Balance);
                    detailDto.Role = role;
                }
            }
            return detailDto;
        }

        public string GetRoleByUsername(string username)
        {
            using (var dbContext = new TrollmarketContext()) {
                var role = dbContext.Accounts.SingleOrDefault(acc => acc.Username == username).Role;

                return role;      
            }

        }

        public void TopUp(TopUpDTO dto, string username)
        {


            using (var dbContext = new TrollmarketContext()) {
                var money = dto.TopUp;
                var entity = dbContext.Buyers.SingleOrDefault(buy => buy.Username == username);
                if (entity.Balance == null)
                {
                    entity.Balance = money;
                    dbContext.SaveChanges();
                }
                else { 
                    entity.Balance = entity.Balance + money;
                    dbContext.SaveChanges();
                }
                
            }
        }
    }
}
