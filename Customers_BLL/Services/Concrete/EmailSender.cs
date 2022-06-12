using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Customers_BLL.Configurations;
using Customers_BLL.Helpers;
using Customers_BLL.Services.Abstract;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace Customers_BLL.Services.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration emailConfig;
        
        public EmailSender(EmailConfiguration emailConfig)
        {
            this.emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(MessageModel message)
        {
            var emailMessage = CreateEmailMessage(message);
            
            await SendAsync(emailMessage);
        }
        
        private MailMessage CreateEmailMessage(MessageModel message)
        {
            MailMessage mailMessage = new ();
            mailMessage.From = new MailAddress(emailConfig.From);

            foreach (var email in message.To)
            {
                mailMessage.To.Add(email);
            }
            
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Content;
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }
        
        private async Task SendAsync(MailMessage mailMessage)
        {
            SmtpClient smtpClient = new (emailConfig.SmtpServer);
            smtpClient.Port = emailConfig.Port;
            smtpClient.Credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}