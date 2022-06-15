using Businesslayer.Interfaces;
using Commonlayer.Model;
using Repositarylayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserModel AddUser(UserModel userReg)
        {
            try
            {
                return this.userRL.AddUser(userReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public UserLogin LoginUser(string Email, string Password)
        //{
        //    try
        //    {
        //        return this.userRL.LoginUser(Email, Password);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public UserLogin LoginUser(UserLogin userLogin)
        {

            try
            {
                return this.userRL.LoginUser(userLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

        
    
}
