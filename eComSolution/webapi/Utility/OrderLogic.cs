using order.commandservice.Models;
using order.queryservices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using webapi.Models;

namespace webapi.Utility
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderQuery queryService;

        public OrderLogic(IOrderQuery _queries)
        {
            queryService = _queries;
        }

        public OrderModel IntiateOrderCreation(string UserId)
        {
            var order = new OrderModel();
            var cartItems = queryService.GetCartByUserId(UserId).ToList();
            var cartPrice = cartItems.Sum(c => c.Price);
            var deliveryCost = (cartPrice <= 50) ? 10 : 20;

            order.UserId = UserId;
            order.TotalPrice = cartPrice;
            order.DeliveryCost = deliveryCost;
            order.OrderNumber = GenerateOrderNumber(UserId);
            order.Status = "Submitted";

            order.Items = new List<OrderItems>();
            foreach (var item in cartItems)
                order.Items.Add(new OrderItems
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity
                });

            return order;
        }

        string GenerateOrderNumber(string UserId)
        {
            var generator = new Random();
            var orderNum = generator.Next(1, 10000);
            var orderText = UserId.Substring(0, 4).ToUpper();

            return $"{orderText}-{orderNum}";
        }
    }
}
