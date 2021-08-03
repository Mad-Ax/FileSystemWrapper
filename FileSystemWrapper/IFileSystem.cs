namespace Axoft.FileSystemWrapper
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFileSystem
    {
        bool DirectoryExists(string path);

        bool FileExists(string path);

        IEnumerable<string> GetFiles(string path);

        IEnumerable<string> GetDirectories(string path);

        void ClearFlag(string path, FileAttributes attributes);

        bool HasFlag(string path, FileAttributes attributes);

        void CopyFile(string sourcePath, string destinationPath); // TODO: write test for this

        void DeleteFile(string path); // TODO: write test for this
    }
}
