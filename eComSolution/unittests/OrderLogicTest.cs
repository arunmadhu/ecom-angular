using AutoFixture;
using Moq;
using NUnit.Framework;
using order.queryservices.Services;
using System.Collections.Generic;
using System.Linq;
using webapi.Utility;

namespace unittests
{
    public class OrderLogicTest
    {
        private IFixture fixture;
        private Mock<IOrderQuery> orderQuery;

        private IOrderLogic orderLogic;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            orderQuery = new Mock<IOrderQuery>();
            orderLogic = new OrderLogic(orderQuery.Object);
        }

        [Test]
        public void IntiateOrderCreationTest()
        {
            var userid = "arun@gmail.com";
            var cart = fixture.Build<order.queryservices.Models.Cart>().CreateMany(3).AsQueryable();
            cart.ToList()[0].ProductId = 10;

            orderQuery.Setup(x => x.GetCartByUserId(It.Is<string>(u => u.Equals(userid)))).Returns(cart);

            var result = orderLogic.IntiateOrderCreation(userid);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Items.Count(c => c.ProductId == 10) == 1);
        }
    }
}
