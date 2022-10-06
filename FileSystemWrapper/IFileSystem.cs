namespace Axoft.FileSystemWrapper
{
    using System.Collections.Generic;
    using System.IO;
    using Axoft.FileSystemWrapper.Models;

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

        void CreateDirectory(string path); // TODO: write test for this

        void MoveDirectory(string sourceDirName, string destDirName); // TODO: write test for this

        void DeleteDirectory(string directory); // TODO: write test for this

        void MoveFile(string sourceFileName, string destFileName, bool overwrite = false); // TODO: write test for this

        IEnumerable<IFileInfo> EnumerateFiles(string path); // TODO: write test for this

        void DeleteFiles(string path, string searchPattern); // TODO: write test for this
    }
}
