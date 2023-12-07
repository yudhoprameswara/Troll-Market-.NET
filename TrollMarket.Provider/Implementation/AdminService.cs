using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Account;
using TrollMarket.DTO.Admin;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Provider.Implementation
{
    public class AdminService : BaseService, IAdminService
    {
        public void RegisterAdmin(AdminRegisterDTO dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var entity = new Account
            {
                Username = dto.Username,
                Password = hashedPassword,
                Role = "Administrator",
            };
            using (var dbContext = new TrollmarketContext())
            {
                dbContext.Accounts.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
