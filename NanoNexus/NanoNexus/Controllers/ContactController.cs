using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Business.Service.Concretes;
using NanoNexus.Core.Models;
using NanoNexus.ViewModels;
using System.Net.Mail;

namespace NanoNexus.Controllers
{
    public class ContactController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IEmailService _emailService;

        public ContactController(UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }


      



            public IActionResult BookTable()
            {

                return View();
            }


            [HttpPost]
            public IActionResult BookTable(SendMailVM model)
            {
                if (!ModelState.IsValid)
                    return View();

                var smtp = new SmtpClient
                {
                    Host = "tu7uv5kks@code.edu.az",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    
                };

                string emailBody = $@"

<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>New Comment Notification</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }}
        .email-container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background-color: #4CAF50;
            color: #ffffff;
            padding: 10px 0;
            text-align: center;
            border-radius: 8px 8px 0 0;
        }}
        .content {{
            padding: 20px;
        }}
        .content h2 {{
            color: #333333;
        }}
        .content p {{
            color: #555555;
            line-height: 1.6;
        }}
        .content .details {{
            margin-top: 20px;
        }}
        .content .details p {{
            margin: 5px 0;
        }}
        .footer {{
            text-align: center;
            color: #888888;
            padding: 10px 0;
            border-top: 1px solid #dddddd;
            margin-top: 20px;
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""header"">
            <h1>Radios</h1>
        </div>
        <div class=""content"">
            <h2>You have received a new comment!</h2>
            <p>Details of the comment are as follows:</p>
            <div class=""details"">
                <p><strong>Name:</strong> {model.Name}</p>
                <p><strong>Email:</strong> {model.Email}</p>
                
                <p><strong>Message:</strong> {model.Comment}</p>
            </div>
        </div>
        <div class=""footer"">
            <p>This is an automated message. Please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>



                ";

                _emailService.SendEmail(model.Email, "Client Comment", emailBody);


                return RedirectToAction("Index", "Contact");
            }
        }
    }


