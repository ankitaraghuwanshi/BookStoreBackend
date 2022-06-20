using Businesslayer.Interfaces;
using Commonlayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }


        [Authorize(Roles = Role.User)]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.orderBL.AddOrder(orderModel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Order is successfull", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Order failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllOrder")]
        public IActionResult GetAllOrder()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.orderBL.GetAllOrder(userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "all order fetched successful ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry! unable to fetch all orders" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
    }
}

