using Businesslayer.Interfaces;
using Commonlayer.Model;
using Repositarylayer.Interfaces;
using System;
using System.Collections.Generic;

namespace Businesslayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public CartModel AddToCart(CartModel cartmodel, int UserId)
        {
            try
            {
                return this.cartRL.AddToCart(cartmodel, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string RemoveBookFromCart(int CartId)
        {

            try
            {
                return this.cartRL.RemoveBookFromCart(CartId);
            }
            catch (Exception)
            {
                throw;
            }
        }


       
    }
}
