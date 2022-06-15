using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Repositarylayer.Services
{
    public class EmailService
    {
        public static void SendMail(string email, string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("aankitest241@gmail.com", "qoixljsoecjteyva");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.From = new MailAddress("aankitest241@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.IsBodyHtml = true;
                msgObj.Body = $"<!DOCTYPE html>"
                    + "<html>"
                    + "<body style=\"background-color:white;text-align:left;\">" +
                     "<h1 style=\"color:grey;\"> Hello User</h1>" +
                     "<h2 style=\"color:grey;font-size:70%\">click on the below link to recover Password</h2>" +
                     "</body>" +
                     $"http://localhost:4200/reset-password/{token}" +
                     "<body style=\"background-color:white;font-size:50%;text-align:left;\">" +
                     "<h1 style=\"color:grey;\">Regards Bookstore</h1>" +

                    "</html>";


                client.Send(msgObj);
            }
        }
    }
}

