using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class WishListModel
    {
        public int WishListId { get; set; }

        public int BookId { get; set; }
        public int UserId { get; set; }
        public BookModel BookModel { get; set; }
    }
}
