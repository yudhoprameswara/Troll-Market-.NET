using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Profile
{
    public class ProfileTransactionRowDTO
    {
        public string Date { get; set; }

        public string Product { get; set; }

        public int Quantity { get; set; }

        public string Shipment { get; set; }

        public string TotalPrice { get; set; }
    }
}
