using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserRegistration
    {
        public int UserID { get; set; }
        public string? FullName { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? MobileNumber { get; set; }
    }
}
