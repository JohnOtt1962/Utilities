using Utilities.Email.Models;

namespace Utilities.Email.EmailOperations
{
    public interface IEmailFetch
    {
        Task<List<SenderDetails>> GetSenderDetails();
    }
}