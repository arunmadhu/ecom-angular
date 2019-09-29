using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using order.commandservice.Models;
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
        private readonly IOrderCommand commandSerive;
        private readonly IOrderQuery queryService;
        private readonly IOrderLogic orderLogic;
        private readonly IMapper mapper;

        public ecomController(IOrderCommand _commands, IOrderQuery _queries, IOrderLogic _orderLogic, IMapper _mapper)
        {
            commandSerive = _commands;
            queryService = _queries;
            orderLogic = _orderLogic;
            mapper = _mapper;
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            return Ok(mapper.Map<IList<ProductModel>>(queryService.GetAllProducts().ToList()));
        }

        [HttpGet()]
        [Route("product/{productid}")]
        public IActionResult GetProductSpec(int productid)
        {
            return Ok(mapper.Map<ProductModel>(queryService.GetAllProducts().Where(p => p.Id == productid).ToList()));
        }

        [HttpGet()]
        [Route("cart/{usermail}")]
        public IActionResult GetCartForUser(string usermail)
        {
            return Ok(mapper.Map<IList<CartModel>>(queryService.GetCartByUserId(usermail).ToList()));
        }

        [HttpPost]
        [Route("cart")]
        public void SubmitCart([FromBody]CartModel model) 
        {
            var cart = queryService.GetCartByUserId(model.UserEmail).Where(c => c.ProductId == model.ProductId).FirstOrDefault();
            var entity = mapper.Map<order.commandservice.Models.Cart>(model);

            if (cart != null)
                entity.Quantity = cart.Quantity + entity.Quantity;

            commandSerive.SaveCart(entity);
        }

        [HttpPost]
        [Route("order")]
        public IActionResult SubmitOrder([FromBody]string userEmail)
        {
            var entity = orderLogic.CalcualteOrderDetails(userEmail);
            entity.OrderNumber = orderLogic.GenerateOrderNumber(userEmail);
            entity.Status = "Submitted";

            commandSerive.SaveOrder(entity);

            return Ok(entity.OrderNumber);
        }

        [HttpDelete("{id}")]
        [Route("removeitemfromcart")]
        public void Delete(int id)
        {
            commandSerive.DeleteCartById(id);
        }
    }
}
