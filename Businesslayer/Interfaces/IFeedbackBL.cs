using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface IFeedbackBL
    {
        public FeedbackModel AddFeedback(FeedbackModel feedbackmodel, int UserId);
         public List<ViewFeedbackModel> GetDetailsByBookId(int BookId);
    }
}
