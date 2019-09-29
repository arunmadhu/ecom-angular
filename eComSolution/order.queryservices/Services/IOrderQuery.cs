
using order.queryservices.Models;
using System.Linq;

namespace order.queryservices.Services
{
    /// <summary>
    /// Query interface for the ecommerce platform
    /// </summary>
    public interface IOrderQuery
    {
        /// <summary>
        /// Get all the products available in the system
        /// </summary>
        /// <returns>IQueryable<Product></returns>
        IQueryable<Product> GetAllProducts();

        /// <summary>
        /// Get cart details for an user by user id.
        /// </summary>
        /// <param name="userId">User email</param>
        /// <returns>IQueryable<Cart></returns>
        IQueryable<Cart> GetCartByUserId(string userId);

        /// <summary>
        /// Get orders submitted by an user.
        /// </summary>
        /// <param name="userId">User email</param>
        /// <returns>IQueryable<Order></returns>
        IQueryable<Order> GetOrderByUserId(string userId);
    }
}
