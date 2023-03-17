using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public CartModel addNewBookToCart(CartModel cartModel);
        public CartModel UpdateBookQuantity(CartModel cartModel);
        public bool DeleteBookFromCart( int CartId, int userId);
        public List<CartModel> GetAllBooksFromCart(int UserId);

        public CartModel GetCartById(int cartId, int UserId);
    }
}
