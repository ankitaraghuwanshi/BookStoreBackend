using Commonlayer.Model;
using Microsoft.Extensions.Configuration;
using Repositarylayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repositarylayer.Services
{
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public CartModel AddToCart(CartModel cartmodel, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);


                SqlCommand cmd = new SqlCommand("spAddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookQuantity", cartmodel.Quantity);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookId", cartmodel.BookId);
                this.sqlConnection.Open();
                int res = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (res >= 1)
                {
                    return cartmodel;
                }
                else
                {
                    return null;
                }
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
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spDeleteFromCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", CartId);

                this.sqlConnection.Open();
                int res = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (res == 0)
                {
                    return "failed to delete book from cart";
                }
                else
                {
                    return "book deleted from cart successfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }

        }
    }
    
}
