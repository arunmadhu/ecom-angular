using webapi.Models;

namespace webapi.Utility
{
    public interface IOrderLogic
    {
        OrderModel IntiateOrderCreation(string UserId);
    }
}
