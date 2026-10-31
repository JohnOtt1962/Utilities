using Utilities.Email.Models;

namespace Utilities.Email.EmailOperations
{
    public interface IEmail
    {
        Task<bool> SendMail(EmailModel emailModel);
    }
}