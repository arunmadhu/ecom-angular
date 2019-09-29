using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
