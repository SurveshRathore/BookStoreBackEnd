using BusinessLayer.Interface;
using CommonLayer.Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class WishListBL:IWishListBL
    {
        public readonly IWishListRL wishListRL;

        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public bool addBookToWishList(int UserId, int BookId)
        {
            try
            {
                return this.wishListRL.addBookToWishList(UserId, BookId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool DeleteBookFromWishList(int wishListId, int userId)
        {
            try
            {
                return this.wishListRL.DeleteBookFromWishList(wishListId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<WishListModel> GetAllBooksFromWishList(int UserId)
        {
            try
            {
                return this.wishListRL.GetAllBooksFromWishList(UserId);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
