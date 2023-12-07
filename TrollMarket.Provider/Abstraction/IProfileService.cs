using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Profile;

namespace TrollMarket.Provider.Abstraction
{
    public interface IProfileService
    {

        public ProfileIndexDTO GetIndex(int page, string username);

        public void TopUp(TopUpDTO dto, string username);
    }
}
