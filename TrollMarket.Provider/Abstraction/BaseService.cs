using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Entity;
using TrollMarket.DTO.Utility;

namespace TrollMarket.Provider.Abstraction
{
    public class BaseService
    {
        protected const int _totalPage = 10;

        protected static int GetSkip(int page)
        {
            return (page - 1) * _totalPage;
        }

        protected static int GetTotalPage(int totalRows)
        {
            var decimalResult = (double)totalRows / _totalPage;
            return (int)Math.Ceiling(decimalResult);
        }
        public static string ToRupiah(decimal? money)
        {
            var indonesia = CultureInfo.CreateSpecificCulture("id-ID");
            return ((decimal)money).ToString("C2", indonesia);
        }

       
        public static string ToYesNo(bool? service) {
            if (service == true) {
                return "Yes";
            }
            return "No";
        }

        public IEnumerable<DropdownDTO> GetBuyerDropdown() {
            IEnumerable<DropdownDTO> dto = new List<DropdownDTO> 
            {
                new DropdownDTO {  
                                    Text = "No Buyer Selected" ,
                                    Value = null 
                                 }
            };

            using (var dbContext = new TrollmarketContext()) {
                var query = from buy in dbContext.Buyers
                            orderby buy.Name
                            select new DropdownDTO
                            {
                                Value = buy.Id.ToString(),
                                Text = buy.Name
                            };
                dto = dto.Concat(query.ToList());
            }
            return dto;
        }

        public IEnumerable<DropdownDTO> GetSellerDropdown()
        {
            IEnumerable<DropdownDTO> dto = new List<DropdownDTO>
            {
                new DropdownDTO {
                                    Text = "No Seller Selected" ,
                                    Value = null
                                 }
            };

            using (var dbContext = new TrollmarketContext())
            {
                var query = from sel in dbContext.Sellers
                            orderby sel.Name
                            select new DropdownDTO
                            {
                                Value = sel.Id.ToString(),
                                Text = sel.Name
                            };
                dto = dto.Concat(query.ToList());
            }
            return dto;
        }

        public IEnumerable<DropdownDTO> GetShipmentDropdown()
        {
            IEnumerable<DropdownDTO> dto = new List<DropdownDTO>
            {
                new DropdownDTO {
                                    Text = "No Shipper Selected" ,
                                    Value = "0"
                                 }
            };

            using (var dbContext = new TrollmarketContext())
            {
                var query = from sel in dbContext.Shippers
                            orderby sel.Name
                            select new DropdownDTO
                            {
                                Value = sel.Id.ToString(),
                                Text = sel.Name
                            };
                dto = dto.Concat(query.ToList());
            }
            return dto;
        }
    }
}
