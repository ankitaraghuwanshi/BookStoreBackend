using Businesslayer.Interfaces;
using Commonlayer.Model;
using Repositarylayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public AdminLoginModel AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                return this.adminRL.AdminLogin(adminLoginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
