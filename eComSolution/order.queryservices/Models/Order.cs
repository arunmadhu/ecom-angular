using System;
using System.Collections.Generic;

namespace order.queryservices.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int TotalItemCost { get; set; }
        public int DeliveryCost { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
    }
}
