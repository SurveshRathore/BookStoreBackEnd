using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.Controllers
{
    [Route("BookStore/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserBL userManager;
        private readonly ILogger<UserController> logger;
        public const string userName = "userName";
        public const string email = "email";
        public UserController (IUserBL userBl, ILogger<UserController> log)
        {
            this.userManager = userBl;
            this.logger = log;
            logger.LogDebug("Nlog injected with the UserController");
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult SignUp (UserRegistration userRegistration)
        {
            try
            {
                UserRegistration userRegistration1 = this.userManager.AddRegisterUser(userRegistration);
                if (userRegistration1 != null)
                {
                    return this.Ok(new { success = true, message = "User successfully Registered. ", result = userRegistration1 } ); 
                }
                else { 
                    return this.BadRequest(new { success = false, message = "User Registration failed. " });   
                }

            }
            catch(Exception)
            {
                throw;
            }

            
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult userLogin(string EmailID, string Password)
        {
            try
            {
                //HttpContext.Session.SetString(UserName);
                //HttpContext.Session.SetString();
                //HttpContext.Session.SetInt32("userId", UserId);

                string userToken = this.userManager.UserLogin(EmailID, Password);
                
                if (userToken != null)
                {
                    //ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    //IDatabase database = connectionMultiplexer.GetDatabase();
                    //int UserId = Convert.ToInt32( database.StringGet("userId"));
                    //String EmailId = database.StringGet("emailId");
                    //String UserName = database.StringGet("fullName");

                    //setUserSession(UserId, EmailId, UserName);

                    //string userName = HttpContext.Session.GetString("userFullName");
                    //var id = HttpContext.Session.GetInt32("userId");

                    //logger.LogInformation(UserName + " Is LogIn successfully");
                    return this.Ok(new { success = true, message = "User successfully Login. ", result = userToken });
                }
                else
                {
                    logger.LogWarning("Failed to LogIn");
                    return this.BadRequest(new { success = false, message = "Invalid EmailId or password. " });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgetPassword (string EmailId)
        {
            try
            {
                string userToken = this.userManager.UserForgetpassword(EmailId);
                if (userToken != null)
                {
                    return this.Ok(new { success = true, message = "Forget password mail send Successfully.", result = userToken });                  
                    
                }

                else
                {
                    return this.BadRequest(new { success = false, message = "Invalid EmailId or password. " });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize (Roles =Role.User)]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string pass, string confirmPass)
        {
            try
            {
                string emailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (pass.Equals(confirmPass))
                {
                    
                    var result = this.userManager.UserResetpassword(emailId, pass, confirmPass);
                    if (result)
                    {
                        return this.Ok(new { sucess = true, message = "Password changed Successfully.", result = result });
                    }
                    else
                    {
                        return this.BadRequest(new { sucess = false, message = "Password changing Failed" });
                    }
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Password and confirm password must be same" });
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void setUserSession(int UserId, string EmailId, string UserName)
        //{
        //    HttpContext.Session.SetString("userFullName", UserName);
        //    HttpContext.Session.SetString("userEmaild", EmailId);
        //    HttpContext.Session.SetInt32("userId", UserId);
        //}
    }
}
