using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IWishListRL
    {
        public bool addBookToWishList(int UserId, int BookId);
        
        public bool DeleteBookFromWishList(int wishListId, int userId);
        public List<WishListModel> GetAllBooksFromWishList(int UserId);
    }
}
