using Microsoft.AspNetCore.Mvc;
using SMTPEmailService.Models;
using SMTPEmailService.Service;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace SMTPEmailService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService emailService;

        public HomeController(IEmailService emailService)
        {
            this.emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string> { "test1@gmail.com", "test2@gmail.com", "test3@gmail.com" }
            };
            await emailService.SendTestEmail(options);

            return View();
        }

    }
}