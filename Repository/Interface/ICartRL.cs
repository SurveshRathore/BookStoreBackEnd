using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface ICartRL
    {
        public CartModel addNewBookToCart(CartModel cartModel);
        public CartModel UpdateBookQuantity(CartModel cartModel);
        public bool DeleteBookFromCart(int CartId, int userId);
        public List<CartModel> GetAllBooksFromCart(int UserId);

        public CartModel GetCartById(int cartId, int UserId);

        //public BookModel GetAllBooksById(int bookId);
    }
}
