using BusinessLayer.Interface;
using CommonLayer.Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrderBL:IOrderBL
    {
        public readonly IOrderRL orderRL;

        public OrderBL (IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public OrderModel AddOrder(OrderModel orderModel)
        {
            try
            {
                return this.orderRL.AddOrder(orderModel);
            }
            catch
            {
                throw;
            }
        }
        public List<OrderModel> GetAllOrders(int UserId)
        {
            try
            {
                return this.orderRL.GetAllOrders(UserId);
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteOrder(int UserId, int OrderId)
        {
            try
            {
                return this.orderRL.DeleteOrder(UserId, OrderId);
            }
            catch
            {
                throw;
            }
        }
    }
}
