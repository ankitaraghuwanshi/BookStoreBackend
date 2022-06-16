using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositarylayer.Interfaces
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel book);
    }
}
