namespace Utilities.Email
{
    public class EmailConfig
    {
        public required string SenderName { get; set; }
        public required string SenderEmail { get; set; }
        public required string UserName { get; set; }
        public required string AppPassword { get; set; }
        public required string Host { get; set; }
        public required int Port { get; set; }
    }
}
