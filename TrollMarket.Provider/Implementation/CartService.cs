using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Cart;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Provider.Implementation
{
    public class CartService : BaseService, ICartService
    {
        public CartIndexDTO GetIndex(int page, string username)
        {
            var dto = new CartIndexDTO();

            using (var dbContext = new TrollmarketContext()) {
                var query = from cart in dbContext.Purchases
                            where cart.Date == null && 
                            cart.Buyer.Username == username
                            select new CartRowDTO
                            {
                                Id = cart.Id,
                                Product = cart.Product.Name,
                                Quantity = (int)cart.Quantity,
                                Shipment = cart.Shipment.Name,
                                Seller = cart.Product.Seller.Name,
                                TotalPrice = ToRupiah((cart.Product.Price * cart.Quantity) + cart.Shipment.Price)
                            };
                dto.TotalPages = GetTotalPage(query.Count());
                query = query.Skip(GetSkip(page)).Take(_totalPage);
                dto.Grid = query.ToList();
            }
            return dto;
        }

        public bool PurchaseAll(string username)
        {
            decimal? totalPrice;
            decimal balance;
            using (var dbContext = new TrollmarketContext())
            {
                var price = from pur in dbContext.Purchases
                            where pur.Buyer.Username == username && pur.Date == null
                            select new { productPrice = pur.Product.Price,
                                         quantity = pur.Quantity,
                                         shipperPrice = pur.Shipment.Price };
                balance = dbContext.Buyers.SingleOrDefault(buy => buy.Username == username).Balance;
                totalPrice = price.Sum(pro => (pro.productPrice*pro.quantity) + pro.shipperPrice);
            }

            var money = balance - totalPrice;
            if (money >= 0)
            {
                using (var dbContext = new TrollmarketContext())
                {
                    // Kurangin Balance Buyer
                    
                    var entity = dbContext.Buyers.SingleOrDefault(buy => buy.Username == username);
                    entity.Balance = money.Value;

                    // Update Seller Balance
                    
                    var purchase = dbContext.Purchases.Include(pur => pur.Product).Include(pro => pro.Product.Seller)
                        .Where(pur => pur.Buyer.Username == username && pur.Date == null).ToList();

                    foreach (var pur in purchase) {
                       
                        pur.Product.Seller.Balance = pur.Product.Seller.Balance + ( pur.Product.Price.Value * pur.Quantity.Value);
                        pur.Date = DateTime.Now;

                    };
                    dbContext.SaveChanges();
                }
                return true;
            }
            else
            { 
                return false;
            }

        }

        public async Task<Purchase> FindById(int purchaseId)
        {
            Purchase entity = new Purchase();   
            using (var dbContext = new TrollmarketContext()) { 
            
               entity = await dbContext.Purchases.FindAsync(purchaseId);
            
            }
            return entity;
        }

        public void Delete(int id)
        {
            using (var dbContext = new TrollmarketContext()) {
                var entity = dbContext.Purchases.SingleOrDefault(pur => pur.Id == id);
                dbContext.Purchases.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
