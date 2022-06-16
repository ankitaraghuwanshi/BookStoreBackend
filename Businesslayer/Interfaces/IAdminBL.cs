using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface IAdminBL
    {
        public AdminLoginModel AdminLogin(AdminLoginModel adminLoginModel);
    }
}
