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
    public class AdddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        public AdddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.addressBL.AddAddress(addressModel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address added successfully ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to add Address" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel addressModel, int AddressId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.addressBL.UpdateAddress(addressModel, AddressId, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address Updated successfully ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Addreess Updation failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteAddress")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.addressBL.DeleteAddress( AddressId, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address Deleted Successfully ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sooory! Deletion Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

    }
}
