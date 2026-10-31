using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Options;
using MimeKit;
using Utilities.Email.Models;

namespace Utilities.Email.EmailOperations
{
    public class EmailFetch(IOptions<EmailConfig> emailConfig) : IEmailFetch
    {
        private readonly EmailConfig _emailConfig = emailConfig.Value;

        public async Task<List<SenderDetails>> GetSenderDetails()
        {
            List<SenderDetails> senderDetails = new List<SenderDetails>();
            using var client = new ImapClient();

            try
            {
                await client.ConnectAsync(_emailConfig.ImapServer, _emailConfig.ImapPort, true);
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.AppPassword);

                var folder = await client.GetFolderAsync(_emailConfig.GemmaLabel);
                await folder.OpenAsync(FolderAccess.ReadOnly);

                var uids = await folder.SearchAsync(SearchQuery.All);

                if (uids.Count > 0)
                    senderDetails = await GetEmailDetails(uids, folder);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }

            return senderDetails;
        }

        private async Task<List<SenderDetails>> GetEmailDetails(IList<UniqueId> emailIds, IMailFolder folder)
        {
            List<SenderDetails> senders = new List<SenderDetails>();

            foreach (var uid in emailIds)
            {
                MimeMessage message = await folder.GetMessageAsync(uid);

                string fromAddress = string.Empty;
                string? content = message.TextBody ?? message.HtmlBody;
                content = ParseBody(content);

                if (message.From.Mailboxes.FirstOrDefault() is MailboxAddress mailbox)
                    fromAddress = mailbox.Address;

                SenderDetails senderDetails = new SenderDetails
                {
                    EmailId = uid,
                    Content = content,
                    From = fromAddress
                };

                senders.Add(senderDetails);
            }

            return senders;
        }

        private string ParseBody(string htmlBody)
        {
            string returnMessage = htmlBody;
            string[] emailParts = htmlBody.Split("From:", StringSplitOptions.None);


            if (emailParts.Length > 1)
            {
                returnMessage = emailParts[0];
                string[] messageParts = returnMessage.Split("_", StringSplitOptions.None);
                returnMessage = messageParts[0];
            }

            return returnMessage;
        }
    }
}