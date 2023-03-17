using BusinessLayer.Interface;
using CommonLayer.Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        public readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserRegistration AddRegisterUser(UserRegistration userRegistration)
        {
            try
            {
                return this.userRL.AddRegisterUser(userRegistration);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public string UserLogin(string EmailID, string Password)
        {
            try
            {
                return this.userRL.UserLogin(EmailID,Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UserForgetpassword(string EmailID)
        {
            try
            {
                return this.userRL.UserForgetpassword(EmailID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UserResetpassword(string EmailID, string Password, string ConfirmPass)
        {
            try
            {
                return this.userRL.UserResetpassword(EmailID,Password,ConfirmPass);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
