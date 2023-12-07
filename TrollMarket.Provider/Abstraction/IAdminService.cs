using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Account;
using TrollMarket.DTO.Admin;

namespace TrollMarket.Provider.Abstraction
{
    public interface IAdminService
    {
        public void RegisterAdmin(AdminRegisterDTO dto);
    }
}
