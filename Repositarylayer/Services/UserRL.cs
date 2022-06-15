using Commonlayer.Model;
using Microsoft.Extensions.Configuration;
using Repositarylayer.Interfaces;
using System;

using System.Data;
using System.Data.SqlClient;


namespace Repositarylayer.Services
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public UserModel AddUser(UserModel userReg)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);  //for connecting to specific database
                SqlCommand cmd = new SqlCommand("spUserRegister", this.sqlConnection)  //this will hold table credentials
                {
                    CommandType = CommandType.StoredProcedure//passing spname here because we had written sp not query
                };
                cmd.Parameters.AddWithValue("@FullName", userReg.FullName);//adding parameters to sp
                cmd.Parameters.AddWithValue("@Email", userReg.Email);
                cmd.Parameters.AddWithValue("@Password", userReg.Password);
                cmd.Parameters.AddWithValue("@MobileNumber", userReg.MobileNumber);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                //ExecuteNonQuery method is used to execute SQL Command or the storeprocedure performs, INSERT, UPDATE or Delete operations.
                //It doesn't return any data from the database.
                //Instead, it returns an integer specifying the number of rows inserted, updated or deleted.
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return userReg;
                }
                else
                
                    return null;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       

        public UserLogin LoginUser(UserLogin userLogin)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);


                SqlCommand cmd = new SqlCommand("spUserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", userLogin.Email);
                cmd.Parameters.AddWithValue("@Password", userLogin.Password);
                this.sqlConnection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    int UserId = 0;
                    //UserLogin userlogin = new UserLogin();
                    while (rdr.Read())
                    {
                        userLogin.Email = Convert.ToString(rdr["Email"]);
                        userLogin.Password = Convert.ToString(rdr["Password"]);
                        UserId = Convert.ToInt32(rdr["UserId"]);
                    }
                    this.sqlConnection.Close();
                    return userLogin;

                }
                else
                    this.sqlConnection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
    }

}
