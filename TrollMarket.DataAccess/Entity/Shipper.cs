using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Entity
{
    public partial class Shipper
    {
        public Shipper()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool? Service { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
