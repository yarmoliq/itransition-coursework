using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
namespace EmailApp
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Fanfiki Email Confirmation", "bogomaz.dima2013@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 25, false);
                await client.AuthenticateAsync("bogomaz.dima2013@gmail.com", "");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    };
}