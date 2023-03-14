using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using StackExchange.Redis;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository.Service
{
    public class UserRL : IUserRL
    {
        public readonly IConfiguration config;
        public string connectionString;

        public UserRL( IConfiguration configuration)
        {
            this.config = configuration;
            this.connectionString = configuration.GetConnectionString("BookStoreDb");
        }



        public UserRegistration AddRegisterUser(UserRegistration userRegistration)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddUser", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@FullName", userRegistration.FullName);
                    sqlCommand.Parameters.AddWithValue("@EmailId", userRegistration.EmailId);
                    sqlCommand.Parameters.AddWithValue("@Password", EncryptPass(userRegistration.Password));
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", userRegistration.MobileNumber);


                    connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return userRegistration;
                    }
                    else
                        return null;
                }


            }
            catch(Exception)
            {
                throw new Exception();
            }
            finally
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            
        }

        string IUserRL.UserLogin(string EmailID, string Password)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            IDatabase database = connectionMultiplexer.GetDatabase();

            try
            {
                int userId = 0;
                String fullName = "";
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spValidateUser", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    
                    sqlCommand.Parameters.AddWithValue("@EmailId", EmailID);
                    sqlCommand.Parameters.AddWithValue("@Password", EncryptPass(Password));
                   

                    connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    //int result = sqlCommand.ExecuteNonQuery();

                    if (sqlDataReader.HasRows)
                    {
                        while(sqlDataReader.Read())
                        {
                            userId = sqlDataReader.IsDBNull("userId") ? 0 : sqlDataReader.GetInt32("userId");
                            fullName = sqlDataReader.IsDBNull("fullName") ? String.Empty : sqlDataReader.GetString("fullName");
                        }
                        database.StringSet(key:"userId", userId);
                        database.StringSet(key: "emailId", EmailID);
                        database.StringSet(key: "fullName", fullName);

                        string token = GenerateJwtToken(EmailID, userId);
                        return token;
                    }
                    else
                        return null;
                }


            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public string UserForgetpassword(string EmailID)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                UserRegistration userRegistration = new UserRegistration();

                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spValidateEmail", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;


                    sqlCommand.Parameters.AddWithValue("@EmailId", EmailID);

                    connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();


                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            userRegistration.UserID = sqlDataReader.IsDBNull("UserID") ? 0 : sqlDataReader.GetInt32("UserID");
                            userRegistration.FullName = sqlDataReader.IsDBNull("fullName") ? string.Empty : sqlDataReader.GetString("fullName");
                        }
                        string token = GenerateJwtToken(EmailID, userRegistration.UserID);
                        MSMQModel mSMQModel = new MSMQModel();
                        mSMQModel.SendMail(token, EmailID, userRegistration.FullName);
                        return token;
                    }
                    else
                        return null;

                }
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool UserResetpassword(string EmailID, string Password, string ConfirmPass)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                if(Password.Equals(ConfirmPass))
                {
                    using (connection)
                    {
                        SqlCommand sqlCommand = new SqlCommand("spResetPassword", connection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;


                        sqlCommand.Parameters.AddWithValue("@EmailId", EmailID);
                        sqlCommand.Parameters.AddWithValue("@Password", EncryptPass(Password));


                        connection.Open();
                        int result = sqlCommand.ExecuteNonQuery();
                        if(result >= 1)
                        {
                            return true;
                        }
                        return false;
                        

                    }
                }
                else
                {
                    //return "Password and confirm password must be same";
                    return false;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        public string EncryptPass(string pass)
        {
            try
            {
                if(pass != string.Empty)
                {
                    byte[] passBytes = new byte[pass.Length];
                    passBytes = Encoding.UTF8.GetBytes(pass);
                    string encodePass = Convert.ToBase64String(passBytes);
                    return encodePass;
                }
                return "Password is empty";
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        public string GenerateJwtToken(string emailID, int userID)
        {
            try
            {

                var userTokenHandler = new JwtSecurityTokenHandler();
                var userKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config["Jwt:key"]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Email, emailID),
                    new Claim("userID",userID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),

                    SigningCredentials = new SigningCredentials(userKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = userTokenHandler.CreateToken(tokenDescriptor);
                return userTokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }

        }

        //private void setRadisCache()
        //{
        //    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
        //    IDatabase database = connectionMultiplexer.GetDatabase();
        //    database.StringSet();

        //}
    }
}
