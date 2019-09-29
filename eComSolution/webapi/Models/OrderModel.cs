using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class OrderModel
    {
        public string OrderNumber { get; set; }
        public int TotalItemPrice { get; set; }
        public int DeliveryCost { get; set; }
        public string Status { get; set; }
    }
}
