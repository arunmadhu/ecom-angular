using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using order.commandservice.Services;
using order.queryservices.Services;
using webapi.Models;
using webapi.Utility;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ecomController : ControllerBase
    {
        private readonly IOrderCommand commandService;
        private readonly IOrderQuery queryService;
        private readonly IOrderLogic orderLogic;
        private readonly IMapper mapper;

        /// <summary>
        /// api controller constructor
        /// </summary>
        /// <param name="_commands">Command object</param>
        /// <param name="_queries">Query object</param>
        /// <param name="_orderLogic">Order logic object</param>
        /// <param name="_mapper">Automapper</param>
        public ecomController(IOrderCommand _commands, IOrderQuery _queries, IOrderLogic _orderLogic, IMapper _mapper)
        {
            commandService = _commands;
            queryService = _queries;
            orderLogic = _orderLogic;
            mapper = _mapper;
        }

        /// <summary>
        /// Gets all the products available in the system
        /// </summary>
        /// <returns>List<Products></returns>
        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            return Ok(mapper.Map<IList<ProductModel>>(queryService.GetAllProducts().ToList()));
        }

        /// <summary>
        /// Get product details by id
        /// </summary>
        /// <param name="productid">Product ID</param>
        /// <returns>Product</returns>
        [HttpGet()]
        [Route("product/{productid}")]
        public IActionResult GetProductSpec(int productid)
        {
            return Ok(mapper.Map<ProductModel>(queryService.GetAllProducts().Where(p => p.Id == productid).FirstOrDefault()));
        }

        /// <summary>
        /// Get the cart details for an user
        /// </summary>
        /// <param name="usermail">User Email ID</param>
        /// <returns>IList<Cart></Cart></returns>
        [HttpGet()]
        [Route("cart/{usermail}")]
        public IActionResult GetCartForUser(string usermail)
        {
            return Ok(mapper.Map<IList<CartModel>>(queryService.GetCartByUserId(usermail).ToList()));
        }
        
        /// <summary>
        /// Updates the changes to a cart
        /// </summary>
        /// <param name="model">New/Updated cart item</param>
        [HttpPost]
        [Route("cart")]
        public void SubmitCart([FromBody]CartModel model) 
        {
            var cart = queryService.GetCartByUserId(model.UserEmail).Where(c => c.ProductId == model.ProductId).FirstOrDefault();
            var entity = mapper.Map<order.commandservice.Models.Cart>(model);

            if (cart != null)
            {
                entity.Id = cart.Id;
                entity.Quantity = cart.Quantity + entity.Quantity;
            }

            entity.Price = entity.Quantity * entity.Price;

            commandService.SaveCart(entity);
        }

        /// <summary>
        /// Get the order items that can be used for preparing the order
        /// </summary>
        /// <param name="usermail">User ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("prepareorder/{usermail}")]
        public IActionResult PrepareOrder(string usermail)
        {
            var order = orderLogic.IntiateOrderCreation(usermail);
            return Ok(order);
        }
        
        /// <summary>
        /// Creates a new order in the system
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>Order Number generated</returns>
        [HttpPost]
        [Route("order")]
        public void SubmitOrder([FromBody]OrderModel model)
        {
            //Create new order.
            commandService.SaveOrder(mapper.Map<order.commandservice.Models.Order>(model));

            //Clear the cart for the user.
            commandService.DeleteCartByUserId(model.UserId);
        }

        /// <summary>
        /// Removes a cart item from the shopping cart
        /// </summary>
        /// <param name="id">cart id</param>
        [HttpDelete("{id}")]
        [Route("deletecart/{id}")]
        public void Delete(int id)
        {
            commandService.DeleteCartById(id);
        }

        /// <summary>
        /// Gets all the orders for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List<OrderModel></Order></returns>
        [HttpGet]
        [Route("orders/{userId}")]
        public IActionResult GetOrdersForUser(string userId)
        {
            return Ok(mapper.Map<IList<OrderModel>>(queryService.GetOrderByUserId(userId).ToList()));
        }
    }
}
