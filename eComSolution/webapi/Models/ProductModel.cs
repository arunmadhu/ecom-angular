using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Catergory { get; set; }
        public int UnitPrice { get; set; }
        public string Manufaturer { get; set; }
        public int StockQuantity { get; set; }
        public int Rating { get; set; }

    }
}
