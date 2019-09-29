using System;

namespace order.commandservice.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public Product Product { get; set; }
    }
}
