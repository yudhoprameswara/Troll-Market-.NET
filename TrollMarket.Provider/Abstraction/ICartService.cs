using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Cart;

namespace TrollMarket.Provider.Abstraction
{
    public interface ICartService
    {
        public CartIndexDTO GetIndex(int page,string username);

        public bool PurchaseAll(string username);

        public void Delete(int id);
    }
}
