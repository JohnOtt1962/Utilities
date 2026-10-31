using MailKit;

namespace Utilities.Email.EmailOperations
{
    public  interface IEmailDelete
    {
        Task DeleteEmail(UniqueId currentUid);
    }
}