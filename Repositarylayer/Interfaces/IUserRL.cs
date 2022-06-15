using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositarylayer.Interfaces
{
    public interface IUserRL
    {
        public UserModel AddUser(UserModel userReg);

        public UserLogin LoginUser(UserLogin userLogin);
    }
}
