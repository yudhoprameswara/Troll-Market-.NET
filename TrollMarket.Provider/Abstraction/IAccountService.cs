using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Account;

namespace TrollMarket.Provider.Abstraction
{
    public interface IAccountService
    {
        public void Register(RegisterDTO dto);

        public bool IsAuthenticated(LoginDTO dto);

        public string GetRole(string username);
    }
}
