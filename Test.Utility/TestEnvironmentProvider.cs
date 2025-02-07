using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Messerli.TempDirectory;

namespace Messerli.Test.Utility;

public sealed class TestEnvironmentProvider : IDisposable
{
    private const string ResourceDirectoryName = "Resources";
    private const string DirectoryPrefix = "test-environment-";

    private readonly IReadOnlyCollection<TestFile> _testFiles;
    private readonly TempSubdirectory _tempDirectory;

    public TestEnvironmentProvider(IReadOnlyCollection<TestFile> testFiles)
    {
        _testFiles = testFiles;
        _tempDirectory = TempSubdirectory.Create(DirectoryPrefix);

        try
        {
            CopyResources();
        }
        catch (Exception)
        {
            Dispose();
            throw;
        }
    }

    public TestEnvironmentProvider()
    {
        _testFiles = [];
        _tempDirectory = TempSubdirectory.Create(DirectoryPrefix);
    }

    public string RootDirectory => _tempDirectory.FullName;

    public void Dispose()
    {
        RemoveTempResourceLocks();
        _tempDirectory.Dispose();
    }

    private void RemoveTempResourceLocks()
    {
        var filesInTempDirectory = Directory.EnumerateFiles(_tempDirectory.FullName, "*", SearchOption.AllDirectories);
        foreach (var file in filesInTempDirectory)
        {
            File.SetAttributes(file, FileAttributes.Normal);
        }
    }

    private void CopyResources()
    {
        if (HasSubDirectories())
        {
            CreateSubDirectories();
        }

        foreach (var file in _testFiles)
        {
            var sourcePath = GetSourcePath(file);
            var destinationPath = GetDestinationPath(file);

            File.Copy(sourcePath, destinationPath, true);
        }
    }

    private static string GetSourcePath(TestFile testFile)
    {
        return Path.Combine(ResourceDirectoryName, testFile.SourceFilePath);
    }

    private string GetDestinationPath(TestFile testFile)
    {
        return Path.Combine(RootDirectory, testFile.RelativeFilePath);
    }

    private bool HasSubDirectories()
    {
        return HasStringInRelativeTestFilePaths("/") || HasStringInRelativeTestFilePaths("\\");
    }

    private bool HasStringInRelativeTestFilePaths(string search)
    {
        return _testFiles.Any(file => file.RelativeFilePath.Contains(search));
    }

    private void CreateSubDirectories()
    {
        var filesInSubDirectories = FindAllFilesInSubDirectories();
        var directoryNames = ExtractDirectoryNames(filesInSubDirectories);

        foreach (var directoryName in directoryNames)
        {
            Directory.CreateDirectory(Path.Combine(RootDirectory, directoryName));
        }
    }

    private IEnumerable<string> FindAllFilesInSubDirectories()
    {
        return _testFiles
            .Select(file => file.RelativeFilePath)
            .Where(path => path.Contains("/") || path.Contains("\\"));
    }

    private static IEnumerable<string> ExtractDirectoryNames(IEnumerable<string> subfolders)
    {
        return subfolders.Select(ExtractSubDirectory);
    }

    private static string ExtractSubDirectory(string path)
    {
        var slash = path.LastIndexOf("/", StringComparison.Ordinal);
        var backslash = path.LastIndexOf("\\", StringComparison.Ordinal);

        if (slash > backslash)
        {
            return path.Substring(0, slash);
        }

        if (backslash > slash)
        {
            return path.Substring(0, backslash);
        }

        throw new ArgumentException("no subfolders");
    }
}
