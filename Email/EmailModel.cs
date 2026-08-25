namespace Utilities.Email
{
    public class EmailModel
    {
        public required string ToolName { get; set; }
        public required ToolArgs Args { get; set; }
    }

    public class ToolArgs
    {
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}