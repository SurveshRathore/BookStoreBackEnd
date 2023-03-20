
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("BookStore/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController:ControllerBase
    {
        public readonly IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [HttpPost]
        [Route("AddNewBook")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                BookModel bookModel1 = this.bookBL.AddNewBook(bookModel);

                if(bookModel1 != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully", Response = bookModel1 });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("UpdateBook")]
        [Authorize(Roles =Role.Admin)]
        public IActionResult updateBook(int bookId, BookModel bookModel)
        {
            try
            {
                BookModel bookModel1 = this.bookBL.UpdateBook(bookId, bookModel);

                if (bookModel1 != null)
                {
                    return this.Ok(new { success = true, message = "Book updated Successfully", Response = bookModel1 });
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

        [HttpDelete]
        [Route("DeleteBook")]
        [Authorize(Roles =Role.Admin)]
        public ActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = bookBL.DeleteBook(BookId);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Book Deleted Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Deleting Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            try
            {
                var result = bookBL.GetAllBooks();

                if (result != null)
                {
                    return Ok(new { success = true, message = "Getting All Books", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("GetBooksById")]
        public ActionResult GetBooksById(int BookId)
        {
            try
            {
                var result = bookBL.GetAllBooksById(BookId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Getting Book By Id", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
