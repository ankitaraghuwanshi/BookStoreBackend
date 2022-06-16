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
                SqlCommand cmd = new SqlCommand("AddBookTable", this.sqlConnection)
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
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
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
                SqlCommand cmd = new SqlCommand("UpdateBooktable", this.sqlConnection)
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
                cmd.Parameters.AddWithValue("@Quantity", updateBookModel.Quantity);
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
                SqlCommand cmd = new SqlCommand("spDeleteBooks", this.sqlConnection)
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

        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> bookmodel = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spGetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookmodel.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["bookId"]),
                            BookName = reader["bookName"].ToString(),
                            AuthorName = reader["authorName"].ToString(),
                            Rating = Convert.ToInt32(reader["rating"]),
                            TotalView = Convert.ToInt32(reader["totalview"]),
                            DiscountPrice = Convert.ToDecimal(reader["discountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["originalPrice"]),
                            BookDetails = reader["BookDetails"].ToString(),
                            BookImage = reader["bookImage"].ToString(),
                            Quantity =Convert.ToInt32(reader["Quantity"].ToString()),
                        });
                    }

                    this.sqlConnection.Close();
                    return bookmodel;
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

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
               
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spGetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@bookId", BookId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    BookModel model = new BookModel();
                    while (reader.Read())
                    {
                        BookId = Convert.ToInt32(reader["bookId"]);
                        model.BookName = reader["bookName"].ToString();
                        model.AuthorName = reader["authorName"].ToString();
                        model.Rating = Convert.ToInt32(reader["rating"]);
                        model.TotalView = Convert.ToInt32(reader["totalview"]);
                        model.DiscountPrice = Convert.ToInt32(reader["discountPrice"]);
                        model.OriginalPrice = Convert.ToInt32(reader["originalPrice"]);
                        model.BookDetails = reader["BookDetails"].ToString();
                        model.BookImage = reader["bookImage"].ToString();
                        model.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                    }
                    this.sqlConnection.Close();
                    return model;
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
    }
    
}