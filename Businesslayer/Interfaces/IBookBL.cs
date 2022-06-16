using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel book);
    }
}
