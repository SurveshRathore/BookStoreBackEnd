using BusinessLayer.Interface;
using CommonLayer.Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class FeedBackBL : IFeedBackBL
    {
        public readonly IFeedBackRL feedBackRL;

        public FeedBackBL(IFeedBackRL feedBackRL)
        {
            this.feedBackRL = feedBackRL;
        }

        public FeedBackModel AddNewFeedBack(FeedBackModel feedBackModel)
        {
            try
            {
                return this.feedBackRL.AddFeedBack(feedBackModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<FeedBackModel> GetAllFeedBacks(int UserId)
        {
            try
            {
                return this.feedBackRL.GetAllFeedBacks(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public FeedBackModel UpdateFeedBack(FeedBackModel feedBackModel)
        {
            try
            {
                return this.feedBackRL.UpdateFeedBack(feedBackModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteFeedBack(int FeedBackId, int UserId)
        {
            try
            {
                return this.feedBackRL.DeleteFeedBack(FeedBackId, UserId);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
