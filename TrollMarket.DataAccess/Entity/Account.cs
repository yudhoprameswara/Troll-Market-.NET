using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Entity
{
    public partial class Account
    {
        public Account()
        {
            Buyers = new HashSet<Buyer>();
            Sellers = new HashSet<Seller>();
        }

        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Buyer> Buyers { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
    }
}
