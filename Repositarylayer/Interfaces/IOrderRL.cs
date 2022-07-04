using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositarylayer.Interfaces
{
    public interface IOrderRL
    {
        public OrderModel AddOrder(OrderModel orderModel, int UserId);
         public List<ViewOrderModel> GetAllOrder(int UserId);

       
    }
}
