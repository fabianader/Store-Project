using System.Net.Mail;
using System.Net;

namespace StoreProject.Features.User.Services
{
    public class EmailModel
    {
        public EmailModel(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModel email);
    }
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailModel email)
        {
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress("fabian.ader6@gmail.com", "Authentication"),
                To = { email.To },
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = false
            };

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("fabian.ader6@gmail.com", "soqh eboo kuqh qdnr"),
                EnableSsl = true,
            };

            smtpClient.Send(mailMessage);
            await Task.CompletedTask;
        }
    }
}
