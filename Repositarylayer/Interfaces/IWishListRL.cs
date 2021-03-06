using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositarylayer.Interfaces
{
    public interface IWishListRL
    {
        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId);

        public string DeleteWishList(int WishListId, int UserId);

        public List<ViewWishListModel> GetWishlistByUserid(int UserId);
    }
}
