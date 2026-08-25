namespace Utilities.Email
{
    public interface IEmail
    {
        Task<bool> SendMail(EmailModel emailModel);
    }
}