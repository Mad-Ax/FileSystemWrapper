namespace Axoft.FileSystemWrapper.Models
{
    public interface IFileInfo
    {
        string Name { get; }

        string DirectoryName { get; }

        long Length { get; }

        string FullName { get; }
    }
}
