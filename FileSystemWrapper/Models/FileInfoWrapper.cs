namespace Axoft.FileSystemWrapper.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class FileInfoWrapper : IFileInfo
    {
        private FileInfo _file;

        public FileInfoWrapper(FileInfo file)
        {
            _file = file;
        }

        public string Name => _file.Name;

        public string DirectoryName => _file.DirectoryName;

        public long Length => _file.Length;

        public string FullName => _file.FullName;

        public static IEnumerable<IFileInfo> EnumerateFiles(IEnumerable<FileInfo> files)
        {
            return files.Select(f => new FileInfoWrapper(f));
        }
    }
}
