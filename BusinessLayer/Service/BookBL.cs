using BusinessLayer.Interface;
using CommonLayer.Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL : IBookBL
    {
        public readonly IBookRL bookRL;

        public BookBL (IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public BookModel AddNewBook (BookModel bookModel)
        {
            try
            {
                return this.bookRL.addNewBook (bookModel);
                    
            }
            catch(Exception)
            {
                throw;
            }
        }
        public BookModel UpdateBook(int bookId, BookModel bookModel)
        {
            try
            {
                return this.bookRL.UpdateBook(bookId, bookModel);

            }
            catch (Exception)
            {
                throw;
            }
        }
            
public bool DeleteBook(int bookId)
        {
            try
            {
                return this.bookRL.DeleteBook(bookId);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public BookModel GetAllBooksById(int bookId)
        {
            try
            {
                return this.bookRL.GetAllBooksById(bookId);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
