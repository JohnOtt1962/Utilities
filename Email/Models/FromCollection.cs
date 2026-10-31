using MailKit;

namespace Utilities.Email.Models
{
    public class SenderDetails
    {
        public required UniqueId EmailId { get; set; }
        public required string From { get; set; }
        public required string Content { get; set; }
    }
}
