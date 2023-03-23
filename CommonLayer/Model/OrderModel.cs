using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class OrderModel
    {
        public int OrderID { get; set; }

        public int CartID { get; set; }

        public int AddressID { get; set; }
        public int UserID { get; set; }
        public int OrderPrice { get; set; }
    }
}
