using order.commandservice.Models;

namespace order.commandservice.Services
{
    /// <summary>
    /// Commands for the ecommerce DB Context
    /// </summary>
    public interface IOrderCommand
    {
        /// <summary>
        /// Updates the cart details 
        /// </summary>
        /// <param name="item"></param>
        void SaveCart(Cart item);

        /// <summary>
        /// Creates a new order in the system
        /// </summary>
        /// <param name="item"></param>
        void SaveOrder(Order item);

        /// <summary>
        /// Deletes the cart for an user. This is done when order is placed from the cart
        /// </summary>
        /// <param name="userId"></param>
        void DeleteCartByUserId(string userId);

        /// <summary>
        /// Delete cart item from the users shopping cart
        /// </summary>
        /// <param name="cartId"></param>
        void DeleteCartById(int cartId);
    }
}
