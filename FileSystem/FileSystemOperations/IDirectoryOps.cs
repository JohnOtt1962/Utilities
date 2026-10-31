namespace Utilities.FileSystem.FileSystemOperations
{
    public interface IDirectoryOps
    {
        List<string> GetFiles(string path);
        List<string> GetDirectoryList(string path);
        bool DirectoryExists(string path);
        void CreateDirectory(string path);
        int GetFileCount(string path);
        long GetSizeOfParentDirectory(string path);
    }
}