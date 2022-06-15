using Businesslayer.Interfaces;
using Commonlayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("register")]
        public IActionResult AddUser(UserModel userReg)
        {
            try
            {
                UserModel data = this.userBL.AddUser(userReg);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<UserModel>(){ Status = true, Message = "User Added Sucessfully", Data = data });
                }
                return this.Ok(new ResponseModel<UserModel>() { Status = true, Message = "User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new ResponseModel<UserModel> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult LoginUser(UserLogin userLogin)
        {
            try
            {
                var result = this.userBL.LoginUser(userLogin);
                if (result != null)
                    return this.Ok(new ResponseModel<UserLogin>() { Status = true, Message = "Login Successfully", Data = result });
                else
                    return this.BadRequest(new ResponseModel<UserLogin> { Status = false, Message = "Failed to login", Data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
