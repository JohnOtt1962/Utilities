namespace Utilities.FileSystem.Models
{
    public class FileStreamModel : Message
    {
        public long Size { get; set; }
        public string FileName { get; set; }
        public FileStream File { get; set; }
    }
}
