using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IBookRL
    {
        public BookModel addNewBook(BookModel bookModel);
        public BookModel UpdateBook(int bookId, BookModel bookModel);
        public bool DeleteBook(int bookId);
        public List<BookModel> GetAllBooks();

        public BookModel GetAllBooksById(int bookId);
    }
}
