using BusinessLayer.Interface;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("BookStore/[controller]")]
    [ApiController]
    public class AdminController:ControllerBase
    {
        public IAdminBL adminBL;

        public AdminController (IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult adminLogin(string emailId, string password)
        {
            try
            {
                var result = this.adminBL.adminLogin(emailId, password);
                
                if(result != null)
                {
                    return this.Ok(new {success = true, message = "Login successfully", Response = result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Login failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
