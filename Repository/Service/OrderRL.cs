using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace Repository.Service
{
    public class OrderRL : IOrderRL
    {
        public string connectionString;
        public OrderRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreDb");
        }
        public OrderModel AddOrder(OrderModel orderModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddOrder", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", orderModel.UserID);
                    command.Parameters.AddWithValue("@CartID", orderModel.CartID);
                    command.Parameters.AddWithValue("@AddressID", orderModel.AddressID);
                    

                    connection.Open();
                    int AddOrNot = command.ExecuteNonQuery();

                    if (AddOrNot >= 1)
                    {
                        return orderModel;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public bool DeleteOrder(int UserId, int OrderId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteOrder", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderID", OrderId);
                    command.Parameters.AddWithValue("@UserID", UserId);

                    connection.Open();
                    int DeleteOrNot = command.ExecuteNonQuery();

                    if (DeleteOrNot >= 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public List<OrderModel> GetAllOrders(int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spGetAllOrder", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@UserId", UserId);

                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if(sqlDataReader.HasRows)
                {
                    List<OrderModel> orders = new List<OrderModel>();
                    while(sqlDataReader.Read())
                    {
                        OrderModel orderModel = new OrderModel()
                        {
                            OrderID = sqlDataReader.IsDBNull("OrderID") ? 0 : sqlDataReader.GetInt32("OrderID"),
                            CartID = sqlDataReader.IsDBNull("CartID") ? 0 : sqlDataReader.GetInt32("CartID"),
                            AddressID = sqlDataReader.IsDBNull("AddressID") ? 0 : sqlDataReader.GetInt32("AddressID"),
                            UserID = sqlDataReader.IsDBNull("UserID") ? 0 : sqlDataReader.GetInt32("UserID"),
                        };
                        orders.Add(orderModel);
                    }
                    return orders;
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
            
        }
    }
}
