using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }

        public int AddressTypeId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PinCode { get; set; }
        public int UserId { get; set; }
    }
}
