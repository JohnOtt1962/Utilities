using Microsoft.AspNetCore.Http;
using Utilities.FileSystem.Models;

namespace Utilities.FileSystem.FileSystemOperations
{
    public interface IFileStreamOps
    {
        string UploadCopyFileTo(IFormFile formFile, string path);
        FileStreamModel ReadFile(string path);
        FileStreamModel CopyFileTo(FileStreamModel caddy);
    }
}