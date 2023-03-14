using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IAdminRL
    {
        public string AdminLogin(string emailId, string password);

        public AdminRegister NewAdminRegistration (AdminRegister adminRegister);
    }
}
