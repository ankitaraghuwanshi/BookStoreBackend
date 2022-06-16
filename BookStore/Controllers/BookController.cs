using Businesslayer.Interfaces;
using Commonlayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel book)
        {
            try
            {
                var result = this.bookBL.AddBook(book);
                if (result != null)
                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Book Added Successfully", Data = result });
                else
                    return this.BadRequest(new ResponseModel<BookModel> { Status = false, Message = "failed to add book", Data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}