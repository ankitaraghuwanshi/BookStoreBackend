using Commonlayer.Model;
using Experimental.System.Messaging;
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
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public UserModel AddUser(UserModel userReg)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);  //for connecting to specific database
                SqlCommand cmd = new SqlCommand("spUserRegister", this.sqlConnection)  //this will hold table credentials
                {
                    CommandType = CommandType.StoredProcedure//passing spname here because we had written sp not query
                };
                var passwordToEncript = EncodePasswordToBase64(userReg.Password);
                cmd.Parameters.AddWithValue("@FullName", userReg.FullName);//adding parameters to sp
                cmd.Parameters.AddWithValue("@Email", userReg.Email);
                cmd.Parameters.AddWithValue("@Password", passwordToEncript);
                cmd.Parameters.AddWithValue("@MobileNumber", userReg.MobileNumber);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                //ExecuteNonQuery method is used to execute SQL Command or the storeprocedure
                //performs, INSERT, UPDATE or Delete operations.
                //It doesn't return any data from the database.
                //Instead, it returns an integer specifying the number of rows inserted, updated or deleted.
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return userReg;
                }
                else

                    return null;

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
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);


                SqlCommand cmd = new SqlCommand("spUserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var passwordToEncript = EncodePasswordToBase64(userLogin.Password);
                cmd.Parameters.AddWithValue("@Email", userLogin.Email);
                cmd.Parameters.AddWithValue("@Password",passwordToEncript);

                this.sqlConnection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    int UserId = 0;

                    while (rdr.Read())
                    {
                        userLogin.Email = Convert.ToString(rdr["Email"]);
                        passwordToEncript = Convert.ToString(rdr["Password"]);
                        UserId = Convert.ToInt32(rdr["UserId"]);
                    }

                    this.sqlConnection.Close();
                    userLogin.Token = this.GenerateJWTToken(userLogin.Email, UserId);
                    return userLogin;

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
        private string GenerateJWTToken(string Email, int UserId)
        {
            //generate token

            var tokenHandler = new JwtSecurityTokenHandler();//header part
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]//payload part
                {  
                    new Claim(ClaimTypes.Role,"User"),
                    new Claim("Email", Email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),

                SigningCredentials =//signature part
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
        //this function Convert to Encord your Password 
        public string EncodePasswordToBase64(string password) //https://www.c-sharpcorner.com/blogs/how-to-encrypt-or-decrypt-password-using-asp-net-with-c-sharp1
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        //this function Convert to Decord your Password
        //public string DecodeFrom64(string encodedData)
        //{
        //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //    System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //    byte[] todecode_byte = Convert.FromBase64String(encodedData);
        //    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        //    char[] decoded_char = new char[charCount];
        //    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        //    string result = new String(decoded_char);
        //    return result;
        //}

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                };
            }
        }
        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();//header
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]//payload
                {
                    new Claim("email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =//signature
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ForgotPassword(string Email)
        {

            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand com = new SqlCommand("spUserForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                com.Parameters.AddWithValue("@Email", Email);
                this.sqlConnection.Open();
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.HasRows)
                {
                    int UserId = 0;
                    while (rdr.Read())
                    { 
                        Email = Convert.ToString(rdr["Email"]);
                        UserId = Convert.ToInt32(rdr["UserId"]);
                    }

                    this.sqlConnection.Close();
                    MessageQueue BookstoreQ;

                    if (MessageQueue.Exists(@".\Private$\BookstoreQueue"))
                        BookstoreQ = new MessageQueue(@".\Private$\BookstoreQueue");
                    else BookstoreQ = MessageQueue.Create(@".\Private$\BookstoreQueue");

                    Message message = new Message();
                    message.Formatter = new BinaryMessageFormatter();
                    message.Body = GenerateJWTToken(Email, UserId);
                    EmailService.SendMail(Email, message.Body.ToString());
                    BookstoreQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    var token = this.GenerateJWTToken(Email, UserId);

                    return token;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string Email, string newPassword, string confirmPassword)
        {

            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                    SqlCommand com = new SqlCommand("spUserResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    var passwordToEncript = EncodePasswordToBase64(newPassword);
                    com.Parameters.AddWithValue("@Email", Email);
                    com.Parameters.AddWithValue("@Password", passwordToEncript);
                    this.sqlConnection.Open();
                    int i = com.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}







