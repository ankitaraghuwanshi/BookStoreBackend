using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel orderModel, int UserId);

        public List<ViewOrderModel> GetAllOrder(int UserId);
    }
}
