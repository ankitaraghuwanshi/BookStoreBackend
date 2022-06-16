using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositarylayer.Interfaces
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel book);

        public UpdateBookModel UpdateBook(UpdateBookModel updateBookModel);

        public string DeleteBook(int bookId);

        public List<BookModel> GetAllBooks();

        public BookModel GetBookByBookId(int BookId);
    }
}
