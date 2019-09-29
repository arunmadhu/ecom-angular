using order.commandservice.Models;

namespace webapi.Utility
{
    public interface IOrderLogic
    {
        Order CalcualteOrderDetails(string UserId);
        string GenerateOrderNumber(string UserId);
    }
}
