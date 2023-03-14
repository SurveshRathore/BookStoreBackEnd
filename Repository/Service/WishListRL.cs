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
    public class WishListRL:IWishListRL
    {
        public string connectionString;

        public WishListRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreDb");
        }
        public bool addBookToWishList(int UserId, int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                

                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddToWishlist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@bookID", BookId);
                    sqlCommand.Parameters.AddWithValue("@UserID", UserId);

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
        public bool DeleteBookFromWishList(int wishListId, int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {


                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spRemoveFromWishList", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@WishListID", wishListId);
                    sqlCommand.Parameters.AddWithValue("@UserID", userId);

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >=1)
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
        public List<WishListModel> GetAllBooksFromWishList(int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                List<WishListModel> list = new List<WishListModel>();

                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetFromWishList", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    
                    sqlCommand.Parameters.AddWithValue("@UserID", UserId);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while(sqlDataReader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            WishListModel model = new WishListModel();
                            model.WishListId = sqlDataReader.IsDBNull("WishListId") ? 0 : sqlDataReader.GetInt32("WishListId");
                            model.BookId = sqlDataReader.IsDBNull("BookId") ? 0 : sqlDataReader.GetInt32("BookId");
                            model.UserId = sqlDataReader.IsDBNull("UserId") ? 0 : sqlDataReader.GetInt32("UserId");

                            //bookModel.BookName = sqlDataReader.IsDBNull("BookName") ? String.Empty : sqlDataReader.GetString("BookName");
                            //bookModel.AuthorName = sqlDataReader.IsDBNull("AuthorName") ? String.Empty : sqlDataReader.GetString("AuthorName");
                            //bookModel.DiscountPrice = sqlDataReader.IsDBNull("DiscountPrice") ? 0 : sqlDataReader.GetInt32("DiscountPrice");
                            //bookModel.OriginalPrice = sqlDataReader.IsDBNull("OriginalPrice") ? 0 : sqlDataReader.GetInt32("OriginalPrice");
                           
                            //model.BookModel = bookModel;
                            list.Add(model);
                        }
                        return list;
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
