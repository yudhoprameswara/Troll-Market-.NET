using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Entity
{
    public partial class Product
    {
        public Product()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public int? SellerId { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Discontinue { get; set; }
        public string? ImagePath { get; set; }

        public virtual Seller? Seller { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
