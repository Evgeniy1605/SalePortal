using SalePortal.Data;
using MimeKit;
using MailKit;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace SalePortal.Domain
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string userEmail, string message)
        {
            string EmailAddress = _configuration.GetSection("EmailSender:Email").Value;
            string Password = _configuration.GetSection("EmailSender:Password").Value;
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("SalePortal", userEmail));
            mimeMessage.To.Add(MailboxAddress.Parse(userEmail));
            mimeMessage.Subject = "Code";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message

            };
            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(EmailAddress, Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
