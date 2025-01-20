using System;
using System.Reflection;

namespace Messerli.Test.Utility;

public sealed class TestFile(string sourceFilePath, string relativeFilePath)
{
    public string SourceFilePath { get; } = sourceFilePath;

    public string RelativeFilePath { get; } = relativeFilePath;

    public static TestFile Create(string filePath)
    {
        return new TestFile(filePath, filePath);
    }

    public static TestFile Create(Assembly assembly, string relativeFilePath)
    {
#pragma warning disable SYSLIB0012
        var uri = new UriBuilder(assembly.CodeBase ?? throw new NullReferenceException("Assembly has no CodeBase"));
#pragma warning restore SYSLIB0012
        return new TestFile(uri.Path, relativeFilePath);
    }
}
