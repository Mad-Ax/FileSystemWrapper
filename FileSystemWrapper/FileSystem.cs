namespace Axoft.FileSystemWrapper
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.IO;

    public class FileSystem : IFileSystem
    {
        private readonly ILogger<FileSystem> _logger;

        public FileSystem(ILogger<FileSystem> logger)
        {
            _logger = logger;
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public void ClearFlag(string path, FileAttributes attributes)
        {
            _logger.LogTrace($"Clearing attributes {attributes} from {path}...");
            File.SetAttributes(path, File.GetAttributes(path) & ~attributes);
        }

        public bool HasFlag(string path, FileAttributes attributes)
        {
            return (File.GetAttributes(path) & attributes) == attributes;
        }

        public void CopyFile(string sourcePath, string destinationPath)
        {
            File.Copy(sourcePath, destinationPath);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
