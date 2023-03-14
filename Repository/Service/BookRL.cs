using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Service
{
    public class BookRL : IBookRL
    {
        public readonly IConfiguration configuration;
        public string connectionString;

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("BookStoreDb");
            
        }

        BookModel IBookRL.addNewBook(BookModel bookModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using(sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddNewBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@BookTotalRating", bookModel.BookTotalRating);
                    sqlCommand.Parameters.AddWithValue("@TotalPeopleRated", bookModel.TotalPeopleRated);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookModel.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    sqlCommand.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if(result >= 1)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

        }

        public BookModel UpdateBook (int bookId, BookModel bookModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookID", bookId);
                    sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@BookTotalRating", bookModel.BookTotalRating);
                    sqlCommand.Parameters.AddWithValue("@TotalPeopleRated", bookModel.TotalPeopleRated);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookModel.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    sqlCommand.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return bookModel;
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
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public bool DeleteBook(int bookId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spDeleteBookById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookID", bookId);
                    

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public List<BookModel> GetAllBooks()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                List<BookModel> books = new List<BookModel>();
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetAllBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();
                    SqlDataReader Reader = sqlCommand.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while(Reader.Read())
                        {
                            BookModel bookModel = new BookModel()
                            {
                                BookID = Reader.IsDBNull("BookID") ? 0 : Reader.GetInt32("BookID"),
                                BookName = Reader.IsDBNull("BookName") ? String.Empty : Reader.GetString("BookName"),
                                AuthorName = Reader.IsDBNull("AuthorName") ? String.Empty : Reader.GetString("AuthorName"),
                                BookTotalRating = Reader.IsDBNull("BookTotalRating") ? 0 : Reader.GetInt32("BookTotalRating"),
                                TotalPeopleRated = Reader.IsDBNull("TotalPeopleRated") ? 0 : Reader.GetInt32("TotalPeopleRated"),
                                DiscountPrice = Reader.IsDBNull("DiscountPrice") ? 0 : Reader.GetInt32("DiscountPrice"),
                                OriginalPrice = Reader.IsDBNull("OriginalPrice") ? 0 : Reader.GetInt32("OriginalPrice"),
                                BookDescription = Reader.IsDBNull("BookDescription") ? String.Empty : Reader.GetString("BookDescription"),
                                BookImage = Reader.IsDBNull("BookImage") ? String.Empty : Reader.GetString("BookImage"),
                                BookQuantity = Reader.IsDBNull("BookQuantity") ? 0 : Reader.GetInt32("BookQuantity"),
                            };
                            books.Add(bookModel);
                        }
                        return books;
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
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public BookModel GetAllBooksById(int bookId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
               
                using (sqlConnection)
                {
                    BookModel bookModel = new BookModel();
                    SqlCommand sqlCommand = new SqlCommand("spGetBookById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookID", bookId);


                    sqlConnection.Open();
                    SqlDataReader Reader = sqlCommand.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {

                            bookModel.BookID = Reader.IsDBNull("BookID") ? 0 : Reader.GetInt32("BookID");
                            bookModel.BookName = Reader.IsDBNull("BookName") ? String.Empty : Reader.GetString("BookName");
                            bookModel.AuthorName = Reader.IsDBNull("AuthorName") ? String.Empty : Reader.GetString("AuthorName");
                            bookModel.BookTotalRating = Reader.IsDBNull("BookTotalRating") ? 0 : Reader.GetInt32("BookTotalRating");
                            bookModel.TotalPeopleRated = Reader.IsDBNull("TotalPeopleRated") ? 0 : Reader.GetInt32("TotalPeopleRated");
                            bookModel.DiscountPrice = Reader.IsDBNull("DiscountPrice") ? 0 : Reader.GetInt32("DiscountPrice");
                            bookModel.OriginalPrice = Reader.IsDBNull("OriginalPrice") ? 0 : Reader.GetInt32("OriginalPrice");
                            bookModel.BookDescription = Reader.IsDBNull("BookDescription") ? String.Empty : Reader.GetString("BookDescription");
                            bookModel.BookImage = Reader.IsDBNull("BookImage") ? String.Empty : Reader.GetString("BookImage");
                            bookModel.BookQuantity = Reader.IsDBNull("BookQuantity") ? 0 : Reader.GetInt32("BookQuantity");
                            
                            
                        }
                        return bookModel;
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
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
