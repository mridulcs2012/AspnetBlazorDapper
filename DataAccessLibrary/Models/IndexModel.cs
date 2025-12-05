using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;


namespace DataAccessLibrary.Models
{
	public class IndexModel
	{
		public void OnGet()
		{

		}

		public void OnPost()
		{
			var name = "Dear user,";
            //var userEmail = Request.Form["emailaddress"];
            var userEmail = "emailaddress";

            //For random OTP
            Random random = new Random();
			var randomNumber = (random.Next(100000, 999999)).ToString();

			SendMail(userEmail, randomNumber);
		}

		public bool SendMail(string toEmail, string randomNumber)
		{
			MailMessage mailMessage = new MailMessage();
			SmtpClient smtpClient = new SmtpClient();
			mailMessage.From = new MailAddress("solimullah.test@gmail.com");
			mailMessage.To.Add(toEmail);
			mailMessage.Subject = "Just for Test.";
			mailMessage.IsBodyHtml = true;
			mailMessage.Body = "<p>Username: " + toEmail + "</p>" + "<p>Verification Code: <strong>" + randomNumber + "</strong></p>";


			smtpClient.Host = "smtp.gmail.com";
			smtpClient.Port = 587;
			smtpClient.EnableSsl = true;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential("solimullah.test@gmail.com", "xcuh crpt ktfv ecur");
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Send(mailMessage);

			return true;
		}
	}
}
