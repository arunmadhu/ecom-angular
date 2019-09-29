using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class OrderModel
    {
        public string UserId { get; set; }
        public string OrderNumber { get; set; }
        public int TotalPrice { get; set; }
        public int DeliveryCost { get; set; }
        public string Status { get; set; }
        public List<OrderItems> Items { get; set; }
    }

    public class OrderItems
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
