using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("BookStore/[Controller]")]
    [ApiController]
    [Authorize(Roles = Role.User)]
    public class FeedBackController:ControllerBase
    {
        public readonly IFeedBackBL feedBackBL;

        public FeedBackController(IFeedBackBL feedBackBL)
        {
            this.feedBackBL = feedBackBL;
        }

        [HttpPost]
        [Authorize(Roles = Role.User)]
        [Route("AddFeedBack")]
        public IActionResult AddFeedBack(FeedBackModel feedBackModel )
        {
            try
            {
                feedBackModel.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);


                FeedBackModel feedBackModel1 = this.feedBackBL.AddNewFeedBack(feedBackModel);

                if (feedBackModel1 != null)
                {
                    return this.Ok(new { success = true, Message = "feedBack Added Successfully", Response = feedBackModel1 });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to Add feedBack" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles = Role.User)]
        [Route("GetAllFeedBack")]
        public IActionResult GetAllFeedBack()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                var result = this.feedBackBL.GetAllFeedBacks(UserId);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "feedback Fetch Successfully", Response = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to get the feedback" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Authorize(Roles = Role.User)]
        [Route("UpdateFeedBack")]
        public IActionResult UpdateFeedBack(FeedBackModel feedBackModel)
        {
            try
            {
                feedBackModel.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                FeedBackModel feedBackModel1 = this.feedBackBL.UpdateFeedBack(feedBackModel);

                if (feedBackModel1 != null)
                {
                    return this.Ok(new { success = true, Message = "FeedBack updated Successfully", Response = feedBackModel1 });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to update Address" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Authorize(Roles = Role.User)]
        [Route("DeleteFeedBack")]
        public IActionResult DeleteFeedBack(int feedBackId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                var result = this.feedBackBL.DeleteFeedBack(UserId, feedBackId);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "feedBack deleted Successfully", Response = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to delete feedBack" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
