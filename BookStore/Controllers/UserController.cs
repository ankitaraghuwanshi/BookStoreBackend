using Businesslayer.Interfaces;
using Commonlayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

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
                    return this.Ok(new ResponseModel<UserModel>() { Status = true, Message = "User Added Sucessfully", Data = data });
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

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var forgotPasswordToken = this.userBL.ForgotPassword(email);
                if (forgotPasswordToken != null)
                {
                    return this.Ok(new { Success = true, message = " Mail Sent Successful", Response = forgotPasswordToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
      
        [Authorize]

        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.Claims.FirstOrDefault(e => e.Type == "Email").Value.ToString();
                if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Unsuccessfully " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }

}


