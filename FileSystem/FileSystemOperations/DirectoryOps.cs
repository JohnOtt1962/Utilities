namespace Utilities.FileSystem.FileSystemOperations
{
    internal class DirectoryOps : IDirectoryOps
    {
        public List<string> GetDirectoryList(string path)
        {
            return Directory.GetDirectories(path).ToList();
        }

        public List<string> GetFiles(string path)
        {
            return Directory.GetFiles(path).ToList();
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public int GetFileCount(string path)
        {
            return Directory.GetFiles(path).Length;
        }

        public long GetSizeOfParentDirectory(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            long dirSize = Task.Run(() => dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length)).Result;
            return dirSize;
        }
    }
}