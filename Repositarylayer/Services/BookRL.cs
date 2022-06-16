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
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public BookModel AddBook(BookModel book)
        {

            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("AddBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookName", book.BookName);
                cmd.Parameters.AddWithValue("@authorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@rating", book.Rating);
                cmd.Parameters.AddWithValue("@totalview", book.TotalView);
                cmd.Parameters.AddWithValue("@originalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@bookdetails", book.BookDetails);
                cmd.Parameters.AddWithValue("@bookImage", book.BookImage);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return book;
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
            finally
            {
                this.sqlConnection.Close();
            }
        }

      

        public UpdateBookModel UpdateBook(UpdateBookModel updateBookModel)
        {

            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("UpdateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookId", updateBookModel.BookId);
                cmd.Parameters.AddWithValue("@BookName", updateBookModel.BookName);
                cmd.Parameters.AddWithValue("@authorName", updateBookModel.AuthorName);
                cmd.Parameters.AddWithValue("@rating", updateBookModel.Rating);
                cmd.Parameters.AddWithValue("@totalview", updateBookModel.TotalView);
                cmd.Parameters.AddWithValue("@originalPrice", updateBookModel.OriginalPrice);
                cmd.Parameters.AddWithValue("@discountPrice", updateBookModel.DiscountPrice);
                cmd.Parameters.AddWithValue("@Bookdetails", updateBookModel.BookDetails);
                cmd.Parameters.AddWithValue("@bookImage", updateBookModel.BookImage);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return updateBookModel;
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
            finally
            {
                this.sqlConnection.Close();
            }

        }

        public string DeleteBook(int bookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spDeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookId", bookId);
               
                this.sqlConnection.Open();
                int res = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (res == 0)
                {
                    return "failed to delete book";
                }
                else
                {
                    return "book deleted successfully";
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
