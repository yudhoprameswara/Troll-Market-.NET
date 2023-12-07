using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Profile
{
    public class ProfileIndexDTO
    {
        public IEnumerable<ProfileTransactionRowDTO> Grid { get; set; }

        public ProfileDetailDTO DetailInfo { get; set; }
        public int TotalPages { get; set; }
    }
}
