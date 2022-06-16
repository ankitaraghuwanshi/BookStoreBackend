using Businesslayer.Interfaces;
using Commonlayer.Model;
using Repositarylayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel AddBook(BookModel book)
        {

            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public UpdateBookModel UpdateBook(UpdateBookModel updateBookModel)
        {
            try
            {
                return this.bookRL.UpdateBook(updateBookModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string DeleteBook(int bookId)
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
    }
}
