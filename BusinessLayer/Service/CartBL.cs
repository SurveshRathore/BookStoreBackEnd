using BusinessLayer.Interface;
using CommonLayer.Model;
using Repository.Interface;
using Repository.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL :ICartBL
    {
        public readonly ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public CartModel addNewBookToCart(CartModel cartModel)
        {
            try
            {
                return this.cartRL.addNewBookToCart(cartModel);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public CartModel UpdateBookQuantity(CartModel cartModel)
        {
            try
            {
                return this.cartRL.UpdateBookQuantity(cartModel);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteBookFromCart(int CartId, int userId)
        {
            try
            {
                return this.cartRL.DeleteBookFromCart(CartId, userId);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CartModel> GetAllBooksFromCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllBooksFromCart(UserId);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public CartModel GetCartById(int cartId, int UserId)
        {
            try
            {
                return this.cartRL.GetCartById(cartId, UserId);
            }
            catch
            {
                throw;
            }
        }


    }
}

