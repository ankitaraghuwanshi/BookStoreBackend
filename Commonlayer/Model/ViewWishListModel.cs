using System;
using System.Collections.Generic;
using System.Text;

namespace Commonlayer.Model
{
    public class ViewWishListModel
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public BookModel Bookmodel { get; set; }
    }
}
