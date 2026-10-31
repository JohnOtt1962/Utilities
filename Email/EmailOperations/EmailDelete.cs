using MailKit;
using MailKit.Net.Imap;
using Microsoft.Extensions.Options;
using Utilities.Email.Models;

namespace Utilities.Email.EmailOperations
{
    public class EmailDelete(IOptions<EmailConfig> emailConfig) : IEmailDelete
    {
        private readonly EmailConfig _emailConfig = emailConfig.Value;

        public async Task DeleteEmail(UniqueId currentUid)
        {
            using var client = new ImapClient();

            try
            {
                await client.ConnectAsync(_emailConfig.ImapServer, _emailConfig.ImapPort, true);
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.AppPassword);

                var labelFolder = await client.GetFolderAsync(_emailConfig.GemmaLabel);
                await labelFolder.OpenAsync(FolderAccess.ReadWrite);

                await labelFolder.AddFlagsAsync(currentUid, MessageFlags.Deleted, silent: true);

                await labelFolder.ExpungeAsync();
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
