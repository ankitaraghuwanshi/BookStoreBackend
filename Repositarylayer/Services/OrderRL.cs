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
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public OrderModel AddOrder(OrderModel orderModel, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("spAddOrders", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookQuantity", orderModel.BookQuantity);
                cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);

                sqlConnection.Open();
                int result = cmd.ExecuteNonQuery();
                sqlConnection.Close();
                if (result == 2)
                {
                    return null;
                }
                else
                {
                    return orderModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ViewOrderModel> GetAllOrder(int UserId)
        {

            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("spGetAllOrders", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                using (sqlConnection)
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<ViewOrderModel> orderViewmodel = new List<ViewOrderModel>();
                        while (reader.Read())
                        {

                            ViewOrderModel ordermodel = new ViewOrderModel();
                            ordermodel.BookId = Convert.ToInt32(reader["BookId"]);
                            ordermodel.BookName = reader["BookName"].ToString();
                            ordermodel.AuthorName = reader["AuthorName"].ToString();

                            ordermodel.OrderDateTime = Convert.ToDateTime(reader["OrderDate"] == DBNull.Value ? default : reader["OrderDate"]);
                            ordermodel.OrderDate = ordermodel.OrderDateTime.ToString("dd-MM-yyyy");
                            ordermodel.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                            ordermodel.BookImage = reader["BookImage"].ToString();
                            ordermodel.UserId = Convert.ToInt32(reader["UserId"]);
                            ordermodel.AddressId = Convert.ToInt32(reader["AddressId"]);
                            ordermodel.OrderId = Convert.ToInt32(reader["OrderId"]);
                            ordermodel.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);

                            orderViewmodel.Add(ordermodel);
                        }

                        sqlConnection.Close();
                        return orderViewmodel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}



    



