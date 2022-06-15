using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface IUserBL
    {
        public UserModel AddUser(UserModel userReg);
        public UserLogin LoginUser(UserLogin userLogin);
    }
}
