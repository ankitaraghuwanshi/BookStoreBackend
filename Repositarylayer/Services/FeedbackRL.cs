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
    public class FeedbackRL : IFeedbackRL
    {
        private SqlConnection sqlConnection;
        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public FeedbackModel AddFeedback(FeedbackModel feedbackmodel, int UserId)
        {
            
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spAddFeedback", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                using (sqlConnection)
                {
                   
                   
                    cmd.Parameters.AddWithValue("@Comment", feedbackmodel.Comment);
                    cmd.Parameters.AddWithValue("@Rating", feedbackmodel.Rating);
                    cmd.Parameters.AddWithValue("@BookId", feedbackmodel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    sqlConnection.Close();
                    if (result == 1)
                    {
                        return feedbackmodel;
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

        public List<ViewFeedbackModel> GetDetailsByBookId(int BookId)
        {

            
            
                try
                {
                   this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                   SqlCommand cmd = new SqlCommand("spGetAllFeedback", this.sqlConnection)
                   {
                    CommandType = CommandType.StoredProcedure
                   };

                

                    cmd.Parameters.AddWithValue("@BookId", BookId);

                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                {
                    List<ViewFeedbackModel> viewFeedbackModel = new List<ViewFeedbackModel>();
                    while (reader.Read())
                        {
                        ViewFeedbackModel feedback = new ViewFeedbackModel();
                        feedback.FeedbackId = Convert.ToInt32(reader["FeedbackId"]);
                        feedback.UserId = Convert.ToInt32(reader["UserId"]);
                        feedback.Rating = Convert.ToInt32(reader["Rating"]);
                        feedback.Comment = reader["Comment"].ToString();
                        feedback.BookId = Convert.ToInt32(reader["BookId"]);
                        feedback.FullName = reader["FullName"].ToString();
                        viewFeedbackModel.Add(feedback);
                    }
                        sqlConnection.Close();
                        return viewFeedbackModel;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        
    }
}
