using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Account;
using TrollMarket.DTO.Utility;
using TrollMarket.Provider.Abstraction;

namespace TrollMarket.Provider.Implementation
{
    public class AccountService : BaseService, IAccountService
    {
        public string GetRole(string username)
        {
            string role;
            using (var dbContext = new TrollmarketContext())
            {
                role = dbContext.Accounts.SingleOrDefault(account => account.Username == username).Role;
            }
            return role;
        }

        public bool IsAuthenticated(LoginDTO dto)
        {
            var isAuthenticated = false;
            using (var dbContext = new TrollmarketContext())
            {
                var entity = dbContext.Accounts.SingleOrDefault(account => account.Username == dto.Username);
                if (entity != null)
                {
                    isAuthenticated = BCrypt.Net.BCrypt.Verify(dto.Password, entity.Password);
                }
            };
            return isAuthenticated;
        }

        public void Register(RegisterDTO dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var entity = new Account
            {
                Username = dto.Username,
                Password = hashedPassword,
                Role = dto.Role.ToString(),
            };
            using (var dbContext = new TrollmarketContext())
            {
                dbContext.Accounts.Add(entity);
                dbContext.SaveChanges();
            }

            if (dto.Role == Role.Seller.ToString())
            {
                RegisterSeller(dto);
            }
            else if (dto.Role == Role.Buyer.ToString()) { 
            
                RegisterBuyer(dto);
            }
        }

        public void RegisterSeller(RegisterDTO dto) {
            var entity = new Seller
            {
                Username = dto.Username,
                Name = dto.Name,
                Address = dto.Address,
                Balance = dto.Balance
            };
            using (var dbContext = new TrollmarketContext())
            {
                dbContext.Sellers.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void RegisterBuyer(RegisterDTO dto)
        {
            var entity = new Buyer
            {
                Username = dto.Username,
                Name = dto.Name,
                Address = dto.Address,
                Balance = dto.Balance
            };
            using (var dbContext = new TrollmarketContext())
            {
                dbContext.Buyers.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
