using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Authorize (Roles =Role.User)]
    [Route("BookStore[Controller]")]
    public class OrderController:ControllerBase
    {
        
        public readonly IOrderBL orderBL;
        private readonly ILogger<OrderController> logger;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        [Authorize (Roles =Role.User)]
        public IActionResult NewOrder(OrderModel orderModel)
        {
            try
            {
                orderModel.UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                OrderModel orderModel1 = this.orderBL.AddOrder(orderModel);
                if (orderModel1 != null)
                {
                    return this.Ok(new { success = true, Message = "Successfully place the order", Response = orderModel1 });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to place the order" });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllOrder")]
        [Authorize(Roles = Role.User)]
        public IActionResult FetchOrder()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                List<OrderModel> orderModel1 = this.orderBL.GetAllOrders(UserID);
                if (orderModel1 != null)
                {
                    return this.Ok(new { success = true, Message = "Successfully fetch the order", Response = orderModel1 });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to fetch the order" });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        [Authorize(Roles = Role.User)]
        public IActionResult DeleteOrder(int OrderId)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                bool result = this.orderBL.DeleteOrder(UserID, OrderId);
                if (result)
                {
                    return this.Ok(new { success = true, Message = "Successfully delete the order", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to delete the order" });
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
