using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using order.commandservice.Context;
using order.commandservice.Models;
using order.commandservice.Services;
using System.Collections.Generic;
using System.Linq;

namespace unittests
{
    public class OrderCommandTest
    {
        private IFixture fixture;
        private Mock<commandContext> dbContext;
        private IOrderCommand command;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            dbContext = new Mock<commandContext>();
            command = new OrderCommand(dbContext.Object);
        }

        [Test]
        public void SaveCartTest()
        {
            var userid = "arun@gmail.com";
            var carts = new List<Cart>().AsQueryable();

            var cartMock = new Mock<DbSet<Cart>>();
            cartMock.As<IQueryable<Cart>>().Setup(m => m.Provider).Returns(carts.Provider);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.Expression).Returns(carts.Expression);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.ElementType).Returns(carts.ElementType);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.GetEnumerator()).Returns(carts.GetEnumerator());

            dbContext.Setup(x => x.Cart).Returns(cartMock.Object);

            command.SaveCart(new Cart { ProductId = 30, UserId = userid });

            cartMock.Verify(m => m.Update(It.IsAny<Cart>()), Times.Once);
            dbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void SaveOrderTest()
        {
            var userid = "arun@gmail.com";
            var orders = new List<Order>().AsQueryable();

            var orderMock = new Mock<DbSet<Order>>();
            orderMock.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
            orderMock.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
            orderMock.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
            orderMock.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

            dbContext.Setup(x => x.Order).Returns(orderMock.Object);

            command.SaveOrder(new Order { UserId = userid, OrderNumber = "TEST123"});

            orderMock.Verify(m => m.Add(It.Is<Order>(o => o.OrderNumber == "TEST123")), Times.Once);
            dbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void DeleteCartByUserIdTest()
        {
            var userid = "arun@gmail.com";
            var carts = new List<Cart>().AsQueryable();

            var cartMock = new Mock<DbSet<Cart>>();
            cartMock.As<IQueryable<Cart>>().Setup(m => m.Provider).Returns(carts.Provider);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.Expression).Returns(carts.Expression);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.ElementType).Returns(carts.ElementType);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.GetEnumerator()).Returns(carts.GetEnumerator());

            dbContext.Setup(x => x.Cart).Returns(cartMock.Object);

            command.DeleteCartByUserId(userid);

            cartMock.Verify(m => m.RemoveRange(It.IsAny<List<Cart>>()), Times.Once);
            dbContext.Verify(m => m.SaveChanges(), Times.Once);

        }
    }
}
