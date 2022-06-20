using Businesslayer.Interfaces;
using Commonlayer.Model;
using Repositarylayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public OrderModel AddOrder(OrderModel orderModel, int UserId)
        {
            try
            {
                return this.orderRL.AddOrder(orderModel, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ViewOrderModel> GetAllOrder(int UserId)
        {

            try
            {
                return this.orderRL.GetAllOrder(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
