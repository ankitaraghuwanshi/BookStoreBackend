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
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(CartModel cartmodel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.cartBL.AddToCart(cartmodel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added in the cart SuccessFully ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to add book in the cart" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteBook/{CartId}")]
        public IActionResult RemoveBookFromCart(int CartId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.cartBL.RemoveBookFromCart(CartId);
                if (result != null)
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book deleted from cart Successfully", Data = result });
                else
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "not deleted from cart", Data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetCartByUserid")]
        public IActionResult GetCartByUserid()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartdata = this.cartBL.GetCartByUserid(UserId);
                if (cartdata != null)
                {
                    return this.Ok(new { Success = true, message = "cart Detail Fetched Sucessfully", Response = cartdata });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter valid userId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }


        }
        [Authorize(Roles = Role.User)]
        [HttpPost("UpdateCart/{CartId}")]
        public IActionResult UpdateCart(CartModel cartModel, int CartId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.cartBL.UpdateCart(cartModel, UserId, CartId);
                if (result != null)
                    return this.Ok(new  { Status = true, message = "cart Updated Successfully", Response = result });
                else
                    return this.BadRequest(new  { Status = false, message = "failed to update cart", Response = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
        