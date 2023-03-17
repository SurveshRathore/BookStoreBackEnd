using BusinessLayer.Interface;
using CommonLayer.Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AddressBL:IAddressBL
    {
        public readonly IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public AddressModel AddNewAddress(AddressModel addressModel)
        {
            try
            {
                return this.addressRL.AddNewAddress(addressModel);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<AddressModel> GetAllAddress(int UserId)
        {
            try
            {
                return this.addressRL.GetAllAddress(UserId);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        bool IAddressBL.DeleteAddress(int AddressId, int UserId)
        {
            throw new NotImplementedException();
        }

        AddressModel IAddressBL.UpdateAddress(AddressModel addressModel)
        {
            throw new NotImplementedException();
        }
    }
}
