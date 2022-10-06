namespace Axoft.FileSystemWrapper
{
    using System.Collections.Generic;
    using System.IO;
    using Axoft.FileSystemWrapper.Models;
    using Microsoft.Extensions.Logging;

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

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void MoveDirectory(string sourceDirName, string destDirName)
        {
            Directory.Move(sourceDirName, destDirName);
        }

        public void DeleteDirectory(string directory)
        {
            Directory.Delete(directory);
        }

        public void MoveFile(string sourceFileName, string destFileName, bool overwrite = false)
        {
            File.Move(sourceFileName, destFileName, overwrite);
        }

        public IEnumerable<IFileInfo> EnumerateFiles(string path)
        {
            return FileInfoWrapper.EnumerateFiles(new DirectoryInfo(path).EnumerateFiles());
        }

        public void DeleteFiles(string path, string searchPattern)
        {
            var files = Directory.GetFiles(path, searchPattern);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
