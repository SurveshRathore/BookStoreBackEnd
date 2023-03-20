using BusinessLayer.Interface;
using CommonLayer.Model;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("BookStore/[Controller]")]
    [ApiController]
    [Authorize(Roles =Role.User)]
    public class AddressController:ControllerBase
    {
        public readonly IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost]
        [Authorize(Roles =Role.User)]
        [Route("AddAddress")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            try
            {
                addressModel.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                AddressModel addressModel1 = this.addressBL.AddNewAddress(addressModel);

                if(addressModel1 != null)
                {
                    return this.Ok(new {success = true, Message = "Address Added Successfully", Response = addressModel1 });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to Add Address" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles = Role.User)]
        [Route("GetAllAddress")]
        public IActionResult GetAddress()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                var result = this.addressBL.GetAllAddress(UserId);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Address Fetch Successfully", Response = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to get the Address" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Authorize(Roles = Role.User)]
        [Route("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel addressModel)
        {
            try
            {
                addressModel.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                AddressModel addressModel1 = this.addressBL.UpdateAddress(addressModel);

                if (addressModel1 != null)
                {
                    return this.Ok(new { success = true, Message = "Address updated Successfully", Response = addressModel1 });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to update Address" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Authorize(Roles = Role.User)]
        [Route("DeleteAddress")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                var result = this.addressBL.DeleteAddress(UserId, AddressId);

                if (result)
                {
                    return this.Ok(new { success = true, Message = "Address deleted Successfully", Response = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to delete Address" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
