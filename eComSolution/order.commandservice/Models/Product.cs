using System;
using System.Collections.Generic;

namespace order.commandservice.Models
{
    public partial class Product
    {
        public Product()
        {
            Cart = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int UnitPrice { get; set; }
        public string Manufacturer { get; set; }
        public int InStock { get; set; }
        public int StarRating { get; set; }

        public ICollection<Cart> Cart { get; set; }
    }
}
