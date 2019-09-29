
using order.commandservice.Context;
using order.commandservice.Models;
using System.Linq;

namespace order.commandservice.Services
{
    /// <summary>
    /// Order commands implementation 
    /// </summary>
    public class OrderCommand : IOrderCommand
    {
        private readonly commandContext commandContext;
        public OrderCommand(commandContext _context)
        {
            commandContext = _context;
        }
        public void SaveCart(Cart item)
        {
            commandContext.Cart.Update(item);
            commandContext.SaveChanges();
        }

        public void SaveOrder(Order item)
        {
            commandContext.Order.Add(item);
            commandContext.SaveChanges();
        }

        public void DeleteCartByUserId(string userId)
        {
            var items = commandContext.Cart.Where(c => c.UserId == userId).ToList();

            commandContext.Cart.RemoveRange(items);
            commandContext.SaveChanges();
        }

        public void DeleteCartById(int cartId)
        {
            commandContext.Cart.RemoveRange(commandContext.Cart.Where(c => c.Id == cartId).ToList());
            commandContext.SaveChanges();
        }
    }
}
