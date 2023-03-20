using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("BookStore/[Controller]")]
    [ApiController]
    [Authorize]
    public class WishListController:ControllerBase
    {
        public readonly IWishListBL wishListBL;

        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddToWishlist")]
        public IActionResult AddToWishList(int BookId)
        {
            try
            {
                var UserId = Convert.ToInt32(this.User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = this.wishListBL.addBookToWishList(UserId, BookId);

                if(result)
                {
                    
                    return Ok(new {success = true, message = "Book Added to whishlist",Response = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to add Book to whishlist" });
                }
                
            }
            catch(Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("RemoveFromWishlist")]
        public IActionResult RemoveWishList(int WishlistId)
        {
            try
            {
                var UserId = Convert.ToInt32(this.User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                bool result = this.wishListBL.DeleteBookFromWishList(UserId, WishlistId);

                if (result)
                {

                    return Ok(new { success = true, message = "Book Removed from whishlist", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to remove Book" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetWishlistDetails")]
        public IActionResult GetWishList()
        {
            try
            {
                var UserId = Convert.ToInt32(this.User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = this.wishListBL.GetAllBooksFromWishList(UserId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Successfully fetched the whishlist", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to get whishlist" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
