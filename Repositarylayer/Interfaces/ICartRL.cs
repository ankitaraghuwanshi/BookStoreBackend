using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositarylayer.Interfaces
{
    public interface ICartRL
    {
        public CartModel AddToCart(CartModel cartmodel, int UserId);

        public string RemoveBookFromCart(int CartId);

        
    }

}
