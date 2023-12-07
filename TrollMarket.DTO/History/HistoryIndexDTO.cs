using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Utility;

namespace TrollMarket.DTO.History
{
    public class HistoryIndexDTO
    {
        public IEnumerable<HistoryRowDTO> Grid { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<DropdownDTO> BuyerDropdown { get; set; }

        public IEnumerable<DropdownDTO> SellerDropdown { get; set; }
    }
}
