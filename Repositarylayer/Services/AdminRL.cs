using Commonlayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositarylayer.Interfaces;
using System;

using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repositarylayer.Services
{
    public class AdminRL : IAdminRL
    {
        private SqlConnection sqlConnection;
        public AdminRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        private string GenerateJWTToken(string Email, int AdminId)
        {
            //generate token

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
                    new Claim("AdminId",AdminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }


        public AdminLoginModel AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);


                SqlCommand cmd = new SqlCommand("AdminLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", adminLoginModel.Email);
                cmd.Parameters.AddWithValue("@Password", adminLoginModel.Password);
                this.sqlConnection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    int AdminId =0 ;

                    while (rdr.Read())
                    {
                        adminLoginModel.Email = Convert.ToString(rdr["Email"]);
                        adminLoginModel.Password = Convert.ToString(rdr["Password"]);
                        AdminId = Convert.ToInt32(rdr["AdminId"]);
                    }

                    this.sqlConnection.Close();
                    var Token = this.GenerateJWTToken(adminLoginModel.Email, AdminId);
                    return adminLoginModel;

                }


                else
                    this.sqlConnection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
