using Microsoft.Extensions.Configuration;
using NanoNexus.Business.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Concretes
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]))
            {
                Credentials = new NetworkCredential(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"]),
                EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]),
            };
            MailMessage mailMessage = new MailMessage()
            {
                Subject = subject,
                From = new MailAddress(_configuration["EmailSettings:Email"]),
                IsBodyHtml = bool.Parse(_configuration["EmailSettings:IsHtml"]),
            };
            mailMessage.To.Add(toEmail);

            mailMessage.Body = body;

            smtpClient.Send(mailMessage);
        }
    }
}
