namespace Utilities.FileSystem.FileSystemOperations
{
    public class FileOps : IFileOps
    {
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public void MoveFile(string source, string destination)
        {
            File.Move(source, destination);
        }

        public void CopyFile(string source, string destination)
        {
            File.Copy(source, destination, true);
        }

        public async Task<string> ReadTextFile(string path)
        {
            return await File.ReadAllTextAsync(path);
        }

        public async Task<string[]> ReadAllLines(string path)
        {
            return await File.ReadAllLinesAsync(path);
        }

        public async Task<byte[]?> ReadAllBytes(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }

        public async Task WriteAllBytes(string path, byte[] bytes)
        {
            await File.WriteAllBytesAsync(path, bytes);
        }

        public async Task WriteAllText(string path, string contents)
        {
            await File.WriteAllTextAsync(path, contents);
        }

        public async Task WriteAllLines(string path, string[] contents)
        {
            await File.WriteAllLinesAsync(path, contents);
        }
    }
}