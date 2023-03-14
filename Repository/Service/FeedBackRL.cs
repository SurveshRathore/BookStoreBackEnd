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
    internal class FeedBackRL : IFeedBackRL
    {
        public string connectionString;

        public FeedBackRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreDb");
        }
        FeedBackModel IFeedBackRL.AddFeedBack(FeedBackModel feedBackModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddFeedBack", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Rating", feedBackModel.Rating);
                    sqlCommand.Parameters.AddWithValue("@Comment", feedBackModel.Comment);
                    sqlCommand.Parameters.AddWithValue("@BookId", feedBackModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", feedBackModel.UserId);

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if(result >= 1)
                    {
                        return feedBackModel;
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
                if(sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            } 

            
        }

        bool IFeedBackRL.DeleteFeedBack(int FeedBackId, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPDeleteFeedBack", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@FeedBackId", FeedBackId);
                    
                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);

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
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }


        }

        List<FeedBackModel> IFeedBackRL.GetAllFeedBacks(int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                List<FeedBackModel> feedbacklist = new List<FeedBackModel>();
                
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetAllFeedBack", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);

                    sqlConnection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FeedBackModel feedBackModel = new FeedBackModel()
                            {
                                FeedBackId = reader.IsDBNull("FeedBackId") ? 0 : reader.GetInt32("FeedBackId"),
                                Rating = reader.IsDBNull("Rating") ? 0 : reader.GetInt32("Rating"),
                                Comment = reader.IsDBNull("Comment") ? String.Empty : reader.GetString("Comment"),
                                BookId = reader.IsDBNull("BookId") ? 0 : reader.GetInt32("BookId"),
                                UserId = reader.IsDBNull("UserId") ? 0 : reader.GetInt32("UserId")
                            };
                            feedbacklist.Add(feedBackModel);
                            
                        }
                        return feedbacklist;
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

        FeedBackModel IFeedBackRL.UpdateFeedBack(FeedBackModel feedBackModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("UpdateFeedBack", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@FeedBackId", feedBackModel.FeedBackId);
                    sqlCommand.Parameters.AddWithValue("@Rating", feedBackModel.Rating);
                    sqlCommand.Parameters.AddWithValue("@Comment", feedBackModel.Comment);
                    sqlCommand.Parameters.AddWithValue("@BookId", feedBackModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", feedBackModel.UserId);

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return feedBackModel;
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
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }


        }
    }
}
