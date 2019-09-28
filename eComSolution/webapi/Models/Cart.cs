using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Cart
    {
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
    }
}
