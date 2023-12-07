using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Merchandise;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Provider.Implementation
{
    public class MerchandiseService : BaseService, IMerchandiseService
    {
        public MerchandiseIndexDTO GetIndex(int page,string username)
        {
            var dto = new MerchandiseIndexDTO();

            using (var dbContext = new TrollmarketContext()) {
                var query = from pro in dbContext.Products
                            where pro.Seller.Username == username
                            select new MerchandiseRowDTO
                            {
                                Id = pro.Id,
                                Name = pro.Name,
                                Category = pro.Category,
                                Discontinue = (bool)pro.Discontinue? "Yes" : "No"
                            };
                dto.TotalPages = GetTotalPage(query.Count());
                query = query.Skip(GetSkip(page)).Take(_totalPage);
                dto.Grid = query.ToList();
            }
            return dto;
        }
        public UpsertProductDTO FindOne(int id)
        {
            var dto = new UpsertProductDTO();

            using (var dbContext = new TrollmarketContext())
            {
                var entity = dbContext.Products.SingleOrDefault(pro => pro.Id == id);

                dto.Id = entity.Id;
                dto.Name = entity.Name;
                dto.sellerId = entity.SellerId.Value;
                dto.Category = entity.Category;
                dto.Description = entity.Description;
                dto.Price = entity.Price.Value;
                dto.Discontinue = entity.Discontinue.Value;
            }
            return dto;
        }
        public int GetSellerIdByUsername(string username) {

            using (var dbContext = new TrollmarketContext()) {
                var sellerId = dbContext.Sellers.SingleOrDefault(sel => sel.Username == username).Id;
                return sellerId;
            }
        }

        public void Save(UpsertProductDTO dto)
        {
            if (dto.Id > 0)
            {
                using (var dbContext = new TrollmarketContext())
                {
                    var entity = dbContext.Products.SingleOrDefault(pro => pro.Id == dto.Id);
                    entity.Name = dto.Name;
                    entity.SellerId = dto.sellerId;
                    entity.Category = dto.Category;
                    entity.Description = dto.Description;
                    entity.Price = dto.Price;
                    entity.Discontinue = dto.Discontinue;

                    dbContext.SaveChanges();
                }
            }
            else
            {
                // Insert Mode
                var entity = new Product
                {
                    Name = dto.Name,
                    SellerId = dto.sellerId,
                    Category = dto.Category,
                    Description = dto.Description,
                    Price = dto.Price,
                    Discontinue = dto.Discontinue,
                };
                Product result;
                using (var dbContext = new TrollmarketContext())
                {
                    result = dbContext.Products.Add(entity).Entity;
                    dbContext.SaveChanges();
                    dto.Id = result.Id;
                }
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new TrollmarketContext())
            {
                var entity = dbContext.Products.SingleOrDefault(pro => pro.Id == id);
                dbContext.Products.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public int CountDependentProducts(int id)
        {
            int totalProducts = 0;
            using (var dbContext = new TrollmarketContext())
            {
                totalProducts = dbContext.Purchases.Where(product => product.ProductId == id).Count();
            }
            return totalProducts;
        }

        public void Discontinue(int id)
        {
            using (var dbContext = new TrollmarketContext()) {
                var entity = dbContext.Products.SingleOrDefault(pro => pro.Id == id);
                entity.Discontinue = true;
                dbContext.SaveChanges();
            }
        }
    }
}
