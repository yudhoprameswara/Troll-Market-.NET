using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Merchandise;

namespace TrollMarket.Provider.Abstraction
{
    public interface IMerchandiseService
    {
        public MerchandiseIndexDTO GetIndex(int page,string username);

        public UpsertProductDTO FindOne(int id);

        public int GetSellerIdByUsername(string username);

        public void Save(UpsertProductDTO dto);

        public void Delete(int id);
        public int CountDependentProducts(int id);

        public void Discontinue(int id);
    }
}
