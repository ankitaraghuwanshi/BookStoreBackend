using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel book);

        public UpdateBookModel UpdateBook(UpdateBookModel updateBookModel);

        public string DeleteBook(int bookId);
        public BookModel GetBookByBookId(int BookId);
        public List<BookModel> GetAllBooks();
    }
}
