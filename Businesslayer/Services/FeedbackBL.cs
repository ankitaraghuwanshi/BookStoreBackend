using Businesslayer.Interfaces;
using Commonlayer.Model;
using Repositarylayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }
        public FeedbackModel AddFeedback(FeedbackModel feedbackmodel, int UserId)
        {

            try
            {
                return this.feedbackRL.AddFeedback(feedbackmodel, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ViewFeedbackModel> GetDetailsByBookId(int BookId)
        {

            try
            {
                return this.feedbackRL.GetDetailsByBookId(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
