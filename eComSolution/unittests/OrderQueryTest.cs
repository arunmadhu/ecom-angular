using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using order.queryservices.Context;
using order.queryservices.Models;
using order.queryservices.Services;
using System.Collections.Generic;
using System.Linq;

namespace unittests
{
    public class OrderQueryTest
    {
        private IFixture fixture;
        private Mock<queryContext> dbContext;
        private IOrderQuery query;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            dbContext = new Mock<queryContext>();
            query = new OrderQuery(dbContext.Object);
        }

        [Test]
        public void GetAllProductsTest()
        {
            var products = new List<Product>()
            {
                new Product{Id =1, Name="Dell Inspiron", UnitPrice =150 },
                new Product{Id =2, Name="Hp Pavilion", UnitPrice = 100 },
                new Product{Id =3, Name="Mac Air", UnitPrice =350 }
            }.AsQueryable();

            var productMock = new Mock<DbSet<Product>>();
            productMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
            productMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
            productMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
            productMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            dbContext.Setup(x => x.Product).Returns(productMock.Object);

            var productResult = query.GetAllProducts();

            Assert.IsNotNull(productResult);
            Assert.IsTrue(productResult.Count() == 3);
        }

        [Test]
        public void GetOrderByUserIdTest()
        {
            var userid = "arun@gmail.com";

            var orders = new List<Order>()
            {
                new Order{Id = 1, OrderNumber = "TEST123",UserId = userid },
                new Order{Id = 2, OrderNumber = "TEST456",UserId = "a@g.com" },
                new Order{Id = 3, OrderNumber = "TEST789",UserId = userid },
            }.AsQueryable();

            var orderMock = new Mock<DbSet<Order>>();
            orderMock.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
            orderMock.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
            orderMock.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
            orderMock.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

            dbContext.Setup(x => x.Order).Returns(orderMock.Object);

            var orderResult = query.GetOrderByUserId(userid);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Count() == 2);
        }
    }
}
