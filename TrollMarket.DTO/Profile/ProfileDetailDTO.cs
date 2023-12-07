using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Profile
{
    public class ProfileDetailDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Balance { get; set; }

        public string Role { get; set; }
    }
}
