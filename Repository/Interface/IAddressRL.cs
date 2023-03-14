using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IAddressRL
    {
        public AddressModel AddNewAddress(AddressModel addressModel);

        public List<AddressModel> GetAllAddress(int UserId);

        public AddressModel UpdateAddress(AddressModel addressModel);
        public bool DeleteAddress(int AddressId, int UserId);
    }

}
