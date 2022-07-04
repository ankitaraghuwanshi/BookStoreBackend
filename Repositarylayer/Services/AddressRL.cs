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
    public class AddressRL : IAddressRL
    {
        private SqlConnection sqlConnection;
        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public string AddAddress(AddressModel addressModel, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);


                SqlCommand cmd = new SqlCommand("spAddAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                using (sqlConnection)

                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressModel.City);
                    cmd.Parameters.AddWithValue("@State", addressModel.State);
                    cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    sqlConnection.Close();
                    if (result == 2)
                    {
                        return "Please Enter Correct Address TypeId For Adding Address";
                    }
                    else
                    {
                        return "Address Added Successfully";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }



        public AddressModel UpdateAddress(AddressModel addressModel, int AddressId, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);


                SqlCommand cmd = new SqlCommand("spforUpdateAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                using (sqlConnection)
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressModel.City);
                    cmd.Parameters.AddWithValue("@State", addressModel.State);
                    cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return addressModel;
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

        public string DeleteAddress(int AddressId, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spDeleteAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AddressId", AddressId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                sqlConnection.Open();
                int result = cmd.ExecuteNonQuery();
                sqlConnection.Close();
                if (result == 0)
                {
                    return "failed";
                }
                else
                {
                    return "successfull";
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

        public List<AddressModel> GetAllAddresses(int UserId)
        {

            try
            {

                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("GetAllAddresses", this.sqlConnection)
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
                        List<AddressModel> addressmodel = new List<AddressModel>();
                        while (reader.Read())
                        {

                            AddressModel addressModel = new AddressModel();
                            addressModel.TypeId = Convert.ToInt32(reader["TypeId"]);
                            addressModel.Address = reader["Address"].ToString();
                            addressModel.City = reader["City"].ToString();
                            addressModel.State = reader["State"].ToString();
                            UserId = Convert.ToInt32(reader["UserId"]);
                            addressmodel.Add(addressModel);
                        }

                        sqlConnection.Close();
                        return addressmodel;
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
