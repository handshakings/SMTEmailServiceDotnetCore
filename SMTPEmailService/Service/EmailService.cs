using SMTPEmailService.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SMTPEmailService.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel smtpConfig;

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            //The IOptions service is used to bind strongly types options class to configuration section
            //and registers it to the Asp.Net Core Dependency Injection Service Container as singleton lifetime.
            //It exposes a Value property which contains your configured TOptions class.
            this.smtpConfig = smtpConfig.Value;
        }
        public async Task SendTestEmail(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = "This is test email subject from John Smith";
            emailOptions.Body = GetEmailBody("TestEmail");
            await SendEmail(emailOptions);
        }


        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {         
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(smtpConfig.SenderAddress, smtpConfig.SenderDisplayName),
                IsBodyHtml = smtpConfig.IsBodyHTML,
                BodyEncoding = Encoding.Default
            };
            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            SmtpClient smtpClient = new SmtpClient
            {
                Host = smtpConfig.Host,
                Port = smtpConfig.Port,
                EnableSsl = smtpConfig.EnableSSL,
                Credentials = new NetworkCredential(smtpConfig.UserName, smtpConfig.Password)
            };
            
            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

    }
}
