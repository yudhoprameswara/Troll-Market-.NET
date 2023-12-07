using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Merchandise
{
    public class MerchandiseIndexDTO
    {
        public IEnumerable<MerchandiseRowDTO> Grid { get; set; }

        public int TotalPages { get; set; }
    }
}
