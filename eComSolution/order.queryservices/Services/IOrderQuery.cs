
using order.queryservices.Models;
using System.Linq;

namespace order.queryservices.Services
{
    public interface IOrderQuery
    {
        IQueryable<Product> GetAllProducts();

        IQueryable<Cart> GetCartByUserId(string userId);

        IQueryable<Order> GetOrderByUserId(string userId);
    }
}
