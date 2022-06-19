using System;
using System.Collections.Generic;
using System.Text;

namespace Commonlayer.Model
{
    public class AdminLoginModel
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
