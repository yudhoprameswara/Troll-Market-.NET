using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Entity
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public int ShipmentId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Date { get; set; }

        public virtual Buyer Buyer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Shipper Shipment { get; set; } = null!;
    }
}
