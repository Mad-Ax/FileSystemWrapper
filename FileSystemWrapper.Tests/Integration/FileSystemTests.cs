namespace FileSystemWrapper.Tests.Integration
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Axoft.FileSystemWrapper;
    using FakeItEasy;
    using Microsoft.Extensions.Logging;
    using Xunit;

    public class FileSystemWrapperTests
    {
        [Fact]
        public void DirectoryExists_WhenExists_ReturnsTrue()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryPath = Path.Combine(appPath, "TestFiles");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            // Act
            var result = fileSystemWrapper.DirectoryExists(directoryPath);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DirectoryExists_WhenNotExists_ReturnsFalse()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryPath = Path.Combine(appPath, "NoSuchFolder");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            // Act
            var result = fileSystemWrapper.DirectoryExists(directoryPath);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FileExists_WhenExists_ReturnsTrue()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appPath, "TestFiles", "TestTextFile.txt");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            // Act
            var result = fileSystemWrapper.FileExists(filePath);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void FileExists_WhenFileNotExists_ReturnsFalse()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appPath, "TestFiles", "NoSuchFile.txt");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            // Act
            var result = fileSystemWrapper.FileExists(filePath);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FileExists_WhenDirectoryNotExists_ReturnsFalse()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appPath, "NoSuchFolder", "TestTextFile.txt");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            // Act
            var result = fileSystemWrapper.FileExists(filePath);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetFiles_ReturnsFiles()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryPath = Path.Combine(appPath, "TestFiles");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            // Act
            var results = fileSystemWrapper.GetFiles(directoryPath);

            // Assert
            Assert.Equal(2, results.Count());

            var expectedFile1 = Path.Combine(directoryPath, "TestTextFile.txt");
            var expectedFile2 = Path.Combine(directoryPath, "AnotherTextFile.txt");
            Assert.Contains(expectedFile1, results);
            Assert.Contains(expectedFile2, results);
        }

        [Fact]
        public void GetDirectories_ReturnsDirectories()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryPath = Path.Combine(appPath, "TestFiles");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            // Act
            var results = fileSystemWrapper.GetDirectories(directoryPath);

            // Assert
            Assert.Equal(2, results.Count());

            var expectedDirectory1 = Path.Combine(directoryPath, "Directory1");
            var expectedDirectory2 = Path.Combine(directoryPath, "Directory2");
            Assert.Contains(expectedDirectory1, results);
            Assert.Contains(expectedDirectory2, results);
        }

        [Fact]
        public void ClearFlag_ClearsFlag()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appPath, "TestFiles", "TestTextFile.txt");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            if ((File.GetAttributes(filePath) & FileAttributes.Archive) != FileAttributes.Archive)
            {
                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Archive);
            }

            // Act
            fileSystemWrapper.ClearFlag(filePath, FileAttributes.Archive);

            // Assert
            Assert.False((File.GetAttributes(filePath) & FileAttributes.Archive) == FileAttributes.Archive);

        }

        [Fact]
        public void ClearFlag_WhenNotSet_DoesNotSetFlag()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appPath, "TestFiles", "TestTextFile.txt");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            if ((File.GetAttributes(filePath) & FileAttributes.Archive) == FileAttributes.Archive)
            {
                File.SetAttributes(filePath, File.GetAttributes(filePath) & ~FileAttributes.Archive);
            }

            // Act
            fileSystemWrapper.ClearFlag(filePath, FileAttributes.Archive);

            // Assert
            Assert.False((File.GetAttributes(filePath) & FileAttributes.Archive) == FileAttributes.Archive);
        }

        [Fact]
        public void HasFlag_WhenHasFlag_ReturnsTrue()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appPath, "TestFiles", "TestTextFile.txt");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            if ((File.GetAttributes(filePath) & FileAttributes.Archive) != FileAttributes.Archive)
            {
                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Archive);
            }

            // Act
            var result = fileSystemWrapper.HasFlag(filePath, FileAttributes.Archive);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasFlag_WhenHasNotFlag_ReturnsFalse()
        {
            // Arrange
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appPath, "TestFiles", "TestTextFile.txt");

            var fileSystemWrapper = new FileSystem(A.Fake<ILogger<FileSystem>>());

            if ((File.GetAttributes(filePath) & FileAttributes.Archive) == FileAttributes.Archive)
            {
                File.SetAttributes(filePath, File.GetAttributes(filePath) & ~FileAttributes.Archive);
            }

            // Act
            var result = fileSystemWrapper.HasFlag(filePath, FileAttributes.Archive);

            // Assert
            Assert.False(result);
        }
    }
}
