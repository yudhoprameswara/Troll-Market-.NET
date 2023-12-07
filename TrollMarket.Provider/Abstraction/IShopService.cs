using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Shop;

namespace TrollMarket.Provider.Abstraction
{
    public interface IShopService
    {
        public ShopIndexDTO GetIndex(int page, string name, string category);

        public ShopProductDetailDTO GetDetails(int productId);

        public void AddToCart(string username,AddToCartDTO dto);
    }
}
