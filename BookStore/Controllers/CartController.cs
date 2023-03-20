using BusinessLayer.Interface;
using CommonLayer.Model;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("BookStore/[controller]")]
    [ApiController]
    [Authorize(Roles =Role.User)]
    public class CartController:ControllerBase
    {
        public readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost]
        [Route("AddBookToCart")]
        public IActionResult AddBookToCart(CartModel cartModel)
        {
            try
            {
                cartModel.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e=>e.Type == "userID").Value);
                
                CartModel cartModel1 = this.cartBL.addNewBookToCart(cartModel);

                if (cartModel1 != null)
                {
                    return this.Ok(new { success = true, message = "Book Added to cart Successfully", Response = cartModel1 });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateBookInCart")]
        public IActionResult UpdateCart(CartModel cartModel)
        {
            try
            {
                cartModel.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                CartModel cartModel1 = this.cartBL.UpdateBookQuantity(cartModel);

                if (cartModel1 != null)
                {
                    return this.Ok(new { success = true, message = "Cart updated Successfully", Response = cartModel1 });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to update cart" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetCartBook")]
        [Authorize]
        public IActionResult GetAllCartBook()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                List<CartModel> cartBook = this.cartBL.GetAllBooksFromCart(UserId);

                if (cartBook != null)
                {
                    return this.Ok(new { success = true, message = "Cart fetched Successfully", Response = cartBook });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to get cart" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteCartBook")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                bool result = this.cartBL.DeleteBookFromCart(cartId, UserId);

                if (result)
                {
                    return this.Ok(new { success = true, message = "Cart updated Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to update cart" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetCartByID")]
        [Authorize(Roles = Role.User)]
        public IActionResult FetchCartUsingId(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                CartModel cartModel = this.cartBL.GetCartById(cartId, userId);

                if(cartModel != null)
                {
                    return this.Ok(new { success = true, Message = "Successfully fetch the cart using id", Response = cartModel });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Unable to fetch" });
                }
            }
            catch
            {
                throw;
            }
        }


    }
}
