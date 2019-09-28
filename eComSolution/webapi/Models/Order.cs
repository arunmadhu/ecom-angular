using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public int TotalPrice { get; set; }
    }
}
