using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using order.queryservices.Context;
using order.queryservices.Models;

namespace order.queryservices.Services
{
    /// <summary>
    /// Query implementation for the DB context
    /// </summary>
    public class OrderQuery : IOrderQuery
    {
        private readonly queryContext queryContext;

        public OrderQuery(queryContext _queryContext)
        {
            queryContext = _queryContext;
        }

        public IQueryable<Product> GetAllProducts()
        {
            return queryContext.Product;
        }

        public IQueryable<Cart> GetCartByUserId(string userId)
        {
            return queryContext.Cart.Include(c => c.Product).Where(c => c.UserId == userId);
        }

        public IQueryable<Order> GetOrderByUserId(string userId)
        {
            return queryContext.Order.Where(o => o.UserId == userId);
        }
    }
}
