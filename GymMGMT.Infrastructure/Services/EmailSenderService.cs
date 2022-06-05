using GymMGMT.Application.Contracts.Services;
using GymMGMT.Application.Models;
using GymMGMT.Infrastructure.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace GymMGMT.Infrastructure.Services
{
    public class EmailSenderService : IEmailSenderService
    { 
        private readonly EmailConfiguration _configuration;

        public EmailSenderService(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _configuration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<p>{0}</p>", message.Content) };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_configuration.SmtpServer, _configuration.Port, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_configuration.UserName, _configuration.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw new Exception("Sending error");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}