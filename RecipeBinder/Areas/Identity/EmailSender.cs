using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RecipeBinder.Areas.Identity
{
    public class EmailSender: IEmailSender
    {
        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILogger Logger { get; }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Logger.LogError($"{email} {Configuration["EmailAddress"]}");
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress(Configuration["EmailAddress"]),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email));

            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Configuration["EmailAddress"], Configuration["EmailPassword"]),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                await smtp.SendMailAsync(message);
            }
            catch (Exception) { }
        }
    }
}
