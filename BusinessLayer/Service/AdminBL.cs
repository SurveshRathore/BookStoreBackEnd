using BusinessLayer.Interface;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminBL: IAdminBL
    {
        public IAdminRL AdminRL;

        public AdminBL(IAdminRL adminRL)
        {
            AdminRL = adminRL;
        }

        public string adminLogin(string emailId, string password)
        {
            try
            {
                return this.AdminRL.AdminLogin(emailId, password);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
