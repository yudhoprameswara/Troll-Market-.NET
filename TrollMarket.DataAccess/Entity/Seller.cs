using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Entity
{
    public partial class Seller
    {
        public Seller()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal Balance { get; set; }

        public virtual Account? UsernameNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
