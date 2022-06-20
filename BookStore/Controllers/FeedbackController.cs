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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel feedbackmodel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.feedbackBL.AddFeedback(feedbackmodel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "feedback added successfull", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " soory!!! Unable to add feedback failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetDetailsByBookId")]
        public IActionResult GetDetailsByBookId(int BookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var Data = this.feedbackBL.GetDetailsByBookId(BookId);
                if (Data != null)
                {
                    return this.Ok(new { success = true, message = "all feedback fetched successful ", response = Data });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry! unable to fetch all feedback" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
    }
}
