using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddNewBook(BookModel book);
        public BookModel UpdateBook(int bookId, BookModel bookModel);
        public bool DeleteBook(int bookId);
        public List<BookModel> GetAllBooks();

        public BookModel GetAllBooksById(int bookId);
    }
}
