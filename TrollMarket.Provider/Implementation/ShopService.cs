using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Shop;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Provider.Implementation
{
    public class ShopService : BaseService, IShopService
    {
        public void AddToCart(string username,AddToCartDTO dto)
        {
            

            var entity = new Purchase { 
                ProductId = dto.ProductId,
                Quantity= dto.Quantity,
                ShipmentId= dto.ShipmentId,
                Date = null,

                
            };

            using (var dbContext = new TrollmarketContext()) {
                var buyerId = dbContext.Buyers.SingleOrDefault(buy => buy.Username == username).Id;
                entity.BuyerId = buyerId;

                dbContext.Purchases.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public ShopProductDetailDTO GetDetails(int productId)
        {
            var productDetail = new ShopProductDetailDTO();
            using (var dbContext = new TrollmarketContext()) {
                var product = dbContext.Products.Include(pro => pro.Seller).
                    SingleOrDefault(product => product.Id == productId);


                if (product == null)
                {
                    // You can handle this situation according to your needs, for example:
                    throw new ArgumentException($"No product found with id: {productId}");
                }
                productDetail.Name = product.Name;
                productDetail.Category = product.Category;
                productDetail.Description = product.Description;
                productDetail.Price = ToRupiah(product.Price);
                productDetail.SellerName = product.Seller.Name;
                
            }
            return productDetail;
        }

        public ShopIndexDTO GetIndex(int page, string name, string category)
        {
            var dto = new ShopIndexDTO();
            dto.ShipperDropdown = GetShipmentDropdown();
            using (var dbContext = new TrollmarketContext()) {
                IQueryable<Product> product = dbContext.Products;
                if (name != null) { 
                    product = product.Where(p => p.Name.Contains(name));
                }
                if (category != null)
                {
                    product = product.Where(pro=> pro.Category.Contains(category));
                }
                var query = from pro in dbContext.Products
                            where pro.Name.Contains(name)
                            where pro.Category.Contains(category)
                            select new ShopRowDTO { 
                                Id = pro.Id,
                                Name = pro.Name,
                                Price = ToRupiah(pro.Price)
                            };
                dto.TotalPages = GetTotalPage(query.Count());
                query = query.Skip(GetSkip(page)).Take(_totalPage);
                dto.Grid = query.ToList();
            };
            return dto;
        }
    }
}
