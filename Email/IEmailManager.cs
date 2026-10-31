using MailKit;
using Utilities.Email.Models;

namespace Utilities.Email
{
    public interface IEmailManager
    {
        Task<bool> SendMail(EmailModel emailModel);
        Task<List<SenderDetails>> GetSenderDetails();
        Task DeleteEmail(UniqueId currentUid);
    }
}