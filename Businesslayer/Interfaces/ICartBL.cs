using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface ICartBL
    {
        public CartModel AddToCart(CartModel cartmodel, int UserId);
        public string RemoveBookFromCart(int CartId);

        public List<ViewCartModel> GetCartByUserid(int UserId);
        public CartModel UpdateCart(CartModel cartModel, int UserId, int CartId);

    }
}
