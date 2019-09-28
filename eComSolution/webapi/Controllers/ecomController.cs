using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ecomController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [HttpGet()]
        [Route("product/{productid}")]
        public string GetProductSpec(int productid)
        {
            return "value" + productid;
        }

        [HttpGet()]
        [Route("cart/{usermail}")]
        public IActionResult GetCartForUser(string usermail)
        {
            return Ok(new string[] { "value1", usermail });
        }

        [HttpPost]
        [Route("cart")]
        public void SubmitCart([FromBody]Cart model) 
        {
        }

        [HttpPost]
        [Route("order")]
        public IActionResult SubmitOrder([FromBody]Order model)
        {
            var result = new Order() { TotalPrice = model.TotalPrice, OrderNumber = "NSW12345"};
            return Ok(result);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
