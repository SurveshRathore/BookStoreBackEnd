using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedBackBL
    {
        public FeedBackModel AddNewFeedBack(FeedBackModel feedBackModel);

        public List<FeedBackModel> GetAllFeedBacks(int UserId);

        public FeedBackModel UpdateFeedBack(FeedBackModel feedBackModel);
        public bool DeleteFeedBack(int FeedBackId, int UserId);
    }
}
