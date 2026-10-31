namespace Utilities.FileSystem.FileSystemOperations
{
    public interface IFileOps
    {
        void DeleteFile(string path);
        void MoveFile(string source, string destination);
        void CopyFile(string source, string destination);
        Task<string> ReadTextFile(string path);
        Task<string[]> ReadAllLines(string path);
        Task<byte[]?> ReadAllBytes(string path);
        Task WriteAllText(string path, string contents);
        Task WriteAllLines(string path, string[] contents);
        Task WriteAllBytes(string path, byte[] bytes);
    }
}
