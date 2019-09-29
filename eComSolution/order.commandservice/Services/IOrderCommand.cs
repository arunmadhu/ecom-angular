using order.commandservice.Models;

namespace order.commandservice.Services
{
    public interface IOrderCommand
    {
        void SaveCart(Cart item);
        void SaveOrder(Order item);
        void DeleteCartByUserId(string userId);
        void DeleteCartById(int cartId);
    }
}
