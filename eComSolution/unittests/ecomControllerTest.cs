using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using order.commandservice.Services;
using order.queryservices.Services;
using System.Collections.Generic;
using System.Linq;
using webapi.Controllers;
using webapi.Mapper;
using webapi.Models;
using webapi.Utility;

namespace Tests
{
    public class ecomControllerTest
    {
        private IFixture fixture;
        private Mock<IOrderCommand> orderCommand;
        private Mock<IOrderQuery> orderQuery;
        private Mock<IOrderLogic> orderLogic;
        private ecomController ecomapiController;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            orderCommand = new Mock<IOrderCommand>();
            orderQuery = new Mock<IOrderQuery>();
            orderLogic = new Mock<IOrderLogic>();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<eComProfile>();
            });
            var mapper = config.CreateMapper();

            ecomapiController = new ecomController(orderCommand.Object, orderQuery.Object,orderLogic.Object, mapper);
        }

        [Test]
        public void GetProductsTest()
        {
            var products = fixture.Build<order.queryservices.Models.Product>().CreateMany(2).AsQueryable();

            orderQuery.Setup(x => x.GetAllProducts()).Returns(products);

            var okResult = ecomapiController.GetProducts() as OkObjectResult;
            var apiResult = okResult.Value as List<ProductModel>;

            Assert.IsNotNull(apiResult);
            Assert.IsTrue(apiResult.Count == 2);
        }

        [Test]
        public void GetProductSpecTest()
        {
            var products = fixture.Build<order.queryservices.Models.Product>().CreateMany(2).AsQueryable();
            products.ToList()[0].Id = 1;

            orderQuery.Setup(x => x.GetAllProducts()).Returns(products);

            var okResult = ecomapiController.GetProductSpec(1) as OkObjectResult;
            var apiResult = okResult.Value as ProductModel;

            Assert.IsNotNull(apiResult);
            Assert.IsTrue(apiResult.ProductId  == 1);
        }

        [Test]
        public void GetCartForUserTest()
        {
            var userid = "arun@gmail.com";
            var cart = fixture.Build<order.queryservices.Models.Cart>().CreateMany().AsQueryable();
            cart.ToList()[0].ProductId = 10;

            orderQuery.Setup(x => x.GetCartByUserId(It.Is<string>(u => u.Equals(userid)))).Returns(cart);

            var okResult = ecomapiController.GetCartForUser(userid) as OkObjectResult;
            var apiResult = okResult.Value as List<CartModel>;

            Assert.IsNotNull(apiResult);
            Assert.IsTrue(apiResult[0].ProductId == 10);

        }

        [Test]
        public void SubmitCartTest()
        {
            var userid = "arun@gmail.com";
            var callBack = false;

            var cartEntity = fixture.Build<order.queryservices.Models.Cart>().CreateMany().AsQueryable();
            cartEntity.ToList()[0].ProductId = 10;

            var cartModel = fixture.Build<CartModel>().Create();
            cartModel.ProductId = 10;
            cartModel.UserEmail = userid;
            cartModel.Quantity = 2;
            cartModel.Price = 50;

            orderQuery.Setup(x => x.GetCartByUserId(It.Is<string>(u => u.Equals(userid)))).Returns(cartEntity);

            orderCommand.Setup(x => x.SaveCart(It.Is<order.commandservice.Models.Cart>(c => c.ProductId == 10)))
                        .Callback((order.commandservice.Models.Cart c) =>
                            {
                                callBack = true;
                            });

            ecomapiController.SubmitCart(cartModel);

            Assert.AreEqual(callBack,true);
        }
    }
}