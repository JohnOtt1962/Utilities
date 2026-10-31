using MailKit;
using Utilities.Email.EmailOperations;
using Utilities.Email.Models;

namespace Utilities.Email
{
    public class EmailManager(IEmail email, IEmailFetch emailFetch, IEmailDelete emailDelete) : IEmailManager
    {
        public async Task<bool> SendMail(EmailModel emailModel)
        {
            return await email.SendMail(emailModel);
        }

        public async Task<List<SenderDetails>> GetSenderDetails()
        {
            return await emailFetch.GetSenderDetails();
        }

        public async Task DeleteEmail(UniqueId currentUid)
        {
            await emailDelete.DeleteEmail(currentUid);
        }
    }
}
