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

        public string ForgotPassword(string Email)
        {
            try
            {
                return this.userRL.ForgotPassword(Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }

        
    
}
