using Businesslayer.Interfaces;
using Commonlayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("loginAdmin")]
        public IActionResult AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                var result = this.adminBL.AdminLogin(adminLoginModel);
                if (result != null)
                    return this.Ok(new ResponseModel<AdminLoginModel>() { Status = true, Message = "Admin Login Successfully", Data = result });
                else
                    return this.BadRequest(new ResponseModel<AdminLoginModel> { Status = false, Message = "Failed to login", Data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
