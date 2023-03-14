using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace Repository.Service
{
    public class CartRL : ICartRL
    {
        
        public string connectionString;

        public CartRL(IConfiguration configuration)
        {
            
            this.connectionString = configuration.GetConnectionString("BookStoreDb");

        }

        public CartModel addNewBookToCart(CartModel cartModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddToCart", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookQuantity",cartModel.BookQuantity);
                    sqlCommand.Parameters.AddWithValue("@bookID", cartModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserID", cartModel.UserId);
                    

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return cartModel;
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
        public CartModel UpdateBookQuantity(CartModel cartModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateToCart", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookQuantity", cartModel.BookQuantity);
                    sqlCommand.Parameters.AddWithValue("@CartID", cartModel.CartId);
                    sqlCommand.Parameters.AddWithValue("@UserID", cartModel.UserId);


                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return cartModel;
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
        public bool DeleteBookFromCart(int CartId, int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spDeleterFromCart", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@CartID", CartId);
                    sqlCommand.Parameters.AddWithValue("@UserID", userId);

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
        public List<CartModel> GetAllBooksFromCart(int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                List<CartModel> cartList = new List<CartModel>();
                
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetAllFromCart", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserID", UserId);

                    sqlConnection.Open();
                    SqlDataReader Reader = sqlCommand.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            CartModel cartModel = new CartModel()
                            {
                                CartId = Reader.IsDBNull("CartID") ? 0 : Reader.GetInt32("CartID"),
                                BookQuantity = Reader.IsDBNull("BookQuantity") ? 0 : Reader.GetInt32("BookQuantity"),
                                BookId = Reader.IsDBNull("BookId") ? 0 : Reader.GetInt32("BookId"),
                                UserId = Reader.IsDBNull("UserId") ? 0 : Reader.GetInt32("UserId"),
                            };
                            cartList.Add(cartModel);
                            
                            
                        }
                        return cartList;
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

        public CartModel GetCartById (int cartId, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                SqlCommand sqlCommand = new SqlCommand("getCartById", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@CartID", cartId);
                sqlCommand.Parameters.AddWithValue("@UserID", UserId);

                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if(sqlDataReader.HasRows)
                {
                    CartModel cartModel = new CartModel();
                    while (sqlDataReader.Read())
                    {


                        cartModel.CartId = sqlDataReader.IsDBNull("CartId") ? 0 : sqlDataReader.GetInt32("CartId");
                        cartModel.BookQuantity = sqlDataReader.IsDBNull("BookQuantity") ? 0 : sqlDataReader.GetInt32("BookQuantity");
                        cartModel.BookId = sqlDataReader.IsDBNull("BookId") ? 0 : sqlDataReader.GetInt32("BookId");
                        cartModel.UserId = sqlDataReader.IsDBNull("UserId") ? 0 : sqlDataReader.GetInt32("UserId");
                        
                       
                    }
                    return cartModel;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if(sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        
    }
}

