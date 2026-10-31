using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Utilities.Email.Models;

namespace Utilities.Email.EmailOperations
{
    public class Email(IOptions<EmailConfig> emailConfig) : IEmail
    {
        private readonly EmailConfig _emailConfig = emailConfig.Value;

        public async Task<bool> SendMail(EmailModel emailModel)
        {
            bool isSuccess = false;

            using var client = new SmtpClient();

            try
            {
                MimeMessage message = GetEmailMessage(emailModel);

                await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.AppPassword);

                await client.SendAsync(message);
                isSuccess = true;
            }
            finally
            {
                await client.DisconnectAsync(true);
            }

            return isSuccess;
        }

        private MimeMessage GetEmailMessage(EmailModel emailModel)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));
            message.To.Add(new MailboxAddress(emailModel.Args.To, emailModel.Args.To));
            message.Subject = emailModel.Args.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = emailModel.Args.Body,
                TextBody = emailModel.Args.Body
            };
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }
    }
}