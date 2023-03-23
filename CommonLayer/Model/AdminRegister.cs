using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class AdminRegister
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }

        public long MobileNumber { get; set; }
    }
}
