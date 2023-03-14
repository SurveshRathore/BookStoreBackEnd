using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace Repository.Service
{
    public class AddressRL : IAddressRL
    {
        public string connectionString;

        public AddressRL (IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreDb");
        }
        public AddressModel AddNewAddress(AddressModel addressModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                

                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddAddress", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Address1",addressModel.Address1);
                    sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                    sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                    sqlCommand.Parameters.AddWithValue("@Pincode", addressModel.PinCode);
                    sqlCommand.Parameters.AddWithValue("@AddressTypeId", addressModel.AddressTypeId);
                    sqlCommand.Parameters.AddWithValue("@UserId", addressModel.UserId);

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if(result >= 1)
                    {
                        return addressModel;
                    }
                    return null;
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

        public List<AddressModel> GetAllAddress(int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {

                List<AddressModel> addressList = new List<AddressModel>();  
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetAllAddress", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    
                    sqlConnection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AddressModel addressModel = new AddressModel()
                            {
                                AddressId = reader.IsDBNull("AddressId") ? 0 : reader.GetInt32("AddressId"),
                                Address1 = reader.IsDBNull("Address1") ? String.Empty : reader.GetString("Address1"),
                                City = reader.IsDBNull("City") ? String.Empty : reader.GetString("City"),
                                State = reader.IsDBNull("State") ? String.Empty : reader.GetString("State"),
                                PinCode = reader.IsDBNull("PinCode") ? 0 : reader.GetInt32("PinCode"),
                                AddressTypeId = reader.IsDBNull("AddressTypeId") ? 0 : reader.GetInt32("AddressTypeId"),
                                UserId = reader.IsDBNull("UserId") ? 0 : reader.GetInt32("UserId"),

                            };
                            addressList.Add(addressModel);
                        }
                        return addressList;
                    }
                    return null;
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

        public bool DeleteAddress(int AddressId, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPDeleteAddress", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@AddressID", AddressId);
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

        public AddressModel UpdateAddress(AddressModel addressModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {


                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("UpdateAddress", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@AddressId", addressModel.AddressId);
                    sqlCommand.Parameters.AddWithValue("@Address1", addressModel.Address1);
                    sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                    sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                    sqlCommand.Parameters.AddWithValue("@Pincode", addressModel.PinCode);
                    sqlCommand.Parameters.AddWithValue("@AddressTypeId", addressModel.AddressTypeId);
                    sqlCommand.Parameters.AddWithValue("@UserId", addressModel.UserId);

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return addressModel;
                    }
                    return null;
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
