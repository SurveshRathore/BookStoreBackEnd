using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IFeedBackRL
    {
        public FeedBackModel AddFeedBack(FeedBackModel feedBackModel );

        public List<FeedBackModel> GetAllFeedBacks(int UserId);

        public FeedBackModel UpdateFeedBack(FeedBackModel feedBackModel);
        public bool DeleteFeedBack(int FeedBackId, int UserId);
    }
}
