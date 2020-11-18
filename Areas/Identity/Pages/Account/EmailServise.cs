using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace coursework_itransition
{
    public interface IMailer
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailService : IMailer
    {
        private readonly SmtpSettings _smptSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smptSettings = smtpSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_smptSettings.SenderName, _smptSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smptSettings.Server, _smptSettings.Port, false);
                // await client.ConnectAsync(_smptSettings.Server);
                await client.AuthenticateAsync(_smptSettings.Username, _smptSettings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    };
}