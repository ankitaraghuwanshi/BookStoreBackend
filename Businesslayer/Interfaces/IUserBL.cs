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
        public string ForgotPassword(string Email);
        public bool ResetPassword(string email, string newPassword, string confirmPassword);

    }
}
