using Microsoft.AspNetCore.Http;
using Utilities.FileSystem.Models;
using Utilities.Logger;

namespace Utilities.FileSystem.FileSystemOperations
{
    internal class FileStreamOps : IFileStreamOps
    {
        private readonly IUtilLogger _logger;

        public FileStreamOps(IUtilLogger logger)
        {
            _logger = logger;
        }

        public string UploadCopyFileTo(IFormFile formFile, string path)
        {
            string message = null;

            try
            {
                formFile.CopyTo(new FileStream(path, FileMode.Create));
            }
            catch (Exception ex)
            {
                message = "Failed to save uploaded file.";
                //_logger.Error(message, ex, this.GetType().FullName);
            }

            return message;
        }

        public FileStreamModel ReadFile(string path)
        {
            FileStreamModel caddy = new FileStreamModel();

            try
            {
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    caddy.File = file;
                    caddy.Size = file.Length;
                }
            }
            catch (Exception ex)
            {
                caddy.ErrorMessage = "Failed to read file.";
                //_logger.Error(caddy.ErrorMessage, ex, this.GetType().FullName);
            }

            return caddy;
        }

        public FileStreamModel CopyFileTo(FileStreamModel caddy)
        {
            FileStream file = null;

            try
            {
                using (FileStream fileStream = File.Create(caddy.FileName))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        writer.Write(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                caddy.ErrorMessage = "Failed to move file.";
                //_logger.Error(caddy.ErrorMessage, ex, this.GetType().FullName);
            }
            finally
            {
                //if(file != null)
                //{
                //    file.Dispose();
                //}
            }

            return caddy;
        }
    }
}