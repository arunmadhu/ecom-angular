using order.commandservice.Models;
using order.queryservices.Services;
using System;
using System.Linq;

namespace webapi.Utility
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderQuery queryService;

        public OrderLogic(IOrderQuery _queries)
        {
            queryService = _queries;
        }

        public Order CalcualteOrderDetails(string UserId)
        {
            var order = new Order();
            var cartPrice = queryService.GetCartByUserId(UserId).Sum(c => c.Price);
            var deliveryCost = (cartPrice <= 50) ? 10 : 20;

            order.TotalItemCost = cartPrice;
            order.DeliveryCost = deliveryCost;

            return order;
        }

        public string GenerateOrderNumber(string UserId)
        {
            var generator = new Random();
            var orderNum = generator.Next(1, 10000);
            var orderText = UserId.Substring(0, 4).ToUpper();

            return $"{orderText}-{orderNum}";
        }
    }
}
