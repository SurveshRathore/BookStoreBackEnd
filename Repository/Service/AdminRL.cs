using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository.Service
{
    public class AdminRL :IAdminRL
    {
        public readonly IConfiguration configuration;
        public string connectionString;

        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("BookStoreDb");
            
        }

        public string AdminLogin(string emailId, string password)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                int adminId = 0;
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spValidateAdmin", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;


                    sqlCommand.Parameters.AddWithValue("@AdminEmailId", emailId);
                    sqlCommand.Parameters.AddWithValue("@AdminPassword", password);


                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    //int result = sqlCommand.ExecuteNonQuery();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            adminId = sqlDataReader.IsDBNull("adminId") ? 0 : sqlDataReader.GetInt32("adminId");
                        }
                        string token = GenerateJwtToken(emailId, adminId);
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
                sqlConnection.Close();
                
            }
        }

        public string GenerateJwtToken(string emailID, int adminID)
        {
            try
            {

                var TokenHandler = new JwtSecurityTokenHandler();
                var userKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration["Jwt:key"]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Email, emailID),
                        new Claim("adminID", adminID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),

                    SigningCredentials = new SigningCredentials(userKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = TokenHandler.CreateToken(tokenDescriptor);
                return TokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public AdminRegister NewAdminRegistration(AdminRegister adminRegister)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                using(sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAdminReg", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@AdminName", adminRegister.AdminName);
                    sqlCommand.Parameters.AddWithValue("@EmailId", adminRegister.EmailId);
                    sqlCommand.Parameters.AddWithValue("@Password", adminRegister.Password);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", adminRegister.MobileNumber);

                    sqlConnection.Open();

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result >= 1)
                    {
                        return adminRegister;
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
        }
    }
}
