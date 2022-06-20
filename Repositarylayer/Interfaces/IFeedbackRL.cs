using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositarylayer.Interfaces
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeedback(FeedbackModel feedbackmodel, int UserId);
        public List<ViewFeedbackModel> GetDetailsByBookId(int BookId);
    }
}
