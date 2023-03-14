using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IUserRL
    {
        public UserRegistration AddRegisterUser(UserRegistration userRegistration);
        public string UserLogin(string EmailID, string Password);

        public string UserForgetpassword(string EmailID);
        public bool UserResetpassword(string EmailID, string Password, string ConfirmPass);
        
    }
}
