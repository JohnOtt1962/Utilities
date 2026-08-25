using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Utilities.Email
{
    public class Email(IOptions<EmailConfig> emailConfig) : IEmail
    {
        private readonly EmailConfig _emailConfig = emailConfig.Value;

        public async Task<bool> SendMail(EmailModel emailModel)
        {
            bool isSuccess = false;
            MimeMessage message = GetEmailMessage(emailModel);

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.AppPassword);

                    await client.SendAsync(message);
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    //No action at this time
                    //Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    // Cleanly disconnect from the server
                    await client.DisconnectAsync(true);
                }
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