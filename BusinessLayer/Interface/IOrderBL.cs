using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel orderModel);
        public List<OrderModel> GetAllOrders(int UserId);
        public bool DeleteOrder(int UserId, int OrderId);
    }
}
