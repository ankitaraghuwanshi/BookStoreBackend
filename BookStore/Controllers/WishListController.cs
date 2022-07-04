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
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishlistBL;
        public WishListController(IWishListBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddToWishList/{bookId}")]
        public IActionResult AddToWishList(WishListModel wishlistModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.wishlistBL.AddToWishList(wishlistModel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added in the wishlist ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to add book in the wishlist" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteWishList/{WishListId}")]
        public IActionResult DeleteWishList(int WishListId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.wishlistBL.DeleteWishList( WishListId,UserId);
                if (result != null)
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book deleted from wishlist Successfully", Data = result });
                else
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "not deleted from wishlist", Data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetWishlistByUserid")]
        public IActionResult GetWishlistByUserid()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var wishlistdata = this.wishlistBL.GetWishlistByUserid(UserId);
                if (wishlistdata != null)
                {
                    return this.Ok(new { Success = true, message = "wishlist Detail Fetched Sucessfully", Response = wishlistdata });
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
    }
}
