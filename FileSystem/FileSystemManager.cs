using Microsoft.Extensions.Logging;
using Utilities.FileSystem.FileSystemOperations;

namespace Utilities.FileSystem
{
    public class FileSystemManager : IFileSystemManager
    {
        private readonly IDirectoryOps _directoryOps;
        private readonly IFileStreamOps _fileStreamOps;
        private readonly IFileOps _fileOps;

        public FileSystemManager(ILogger logger)
        {
            _directoryOps = new DirectoryOps();
            _fileOps = new FileOps();
        }

        public void DeleteFile(string path)
        {
            _fileOps.DeleteFile(path);
        }

        public async Task<string> ReadTextFile(string path)
        {
            return await _fileOps.ReadTextFile(path);
        }

        public async Task<string[]> ReadAllLines(string path)
        {
            return await _fileOps.ReadAllLines(path);
        }

        public async Task<byte[]?> ReadAllBytes(string path)
        {
            return await _fileOps.ReadAllBytes(path);
        }

        public void MoveFile(string source, string destination)
        {
            _fileOps.MoveFile(source, destination);
        }

        public void CopyFile(string source, string destination)
        {
            _fileOps.CopyFile(source, destination);
        }

        public async Task WriteAllText(string path, string contents)
        {
            await _fileOps.WriteAllText(path, contents);
        }

        public async Task WriteAllLines(string path, string[] contents)
        {
            await _fileOps.WriteAllLines(path, contents);
        }

        public async Task WriteAllBytes(string path, byte[] bytes)
        {
            await _fileOps.WriteAllBytes(path, bytes);
        }

        public List<string> GetDirectoryList(string path)
        {
            return _directoryOps.GetDirectoryList(path);
        }

        public bool DirectoryExists(string path)
        {
            return _directoryOps.DirectoryExists(path);
        }

        public List<string> GetFiles(string path)
        {
            return _directoryOps.GetFiles(path);
        }

        public void CreateDirectory(string path)
        {
            _directoryOps.CreateDirectory(path);
        }

        public int GetFileCount(string path)
        {
            return _directoryOps.GetFileCount(path);
        }

        public long GetSizeOfParentDirectory(string path)
        {
            return _directoryOps.GetSizeOfParentDirectory(path);
        }
    }
}