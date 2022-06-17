using Businesslayer.Interfaces;
using Commonlayer.Model;
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

        [HttpPost("AddToCart/{UserId}")]
        public IActionResult AddToCart(CartModel cartmodel, int UserId)
        {
            try
            {

                var result = this.cartBL.AddToCart(cartmodel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added SuccessFully in the Cart ", response = result });
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

        [HttpDelete("DeleteBook")]
        public IActionResult RemoveBookFromCart(int CartId)
        {
            try
            {
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

        [HttpGet("GetCartByUserid/{UserId}")]
        public IActionResult GetCartByUserid(int UserId)
        {
            try
            {
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

        [HttpPost("UpdateCart/UserId/CartId")]
        public IActionResult UpdateCart(CartModel cartModel, int UserId, int CartId)
        {
            try
            {
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
        