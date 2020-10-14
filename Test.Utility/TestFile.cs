using System;
using System.Reflection;

namespace Messerli.Test.Utility
{
    public sealed class TestFile
    {
        public TestFile(string sourceFilePath, string relativeFilePath)
        {
            SourceFilePath = sourceFilePath;
            RelativeFilePath = relativeFilePath;
        }

        public string SourceFilePath { get; }

        public string RelativeFilePath { get; }

        public static TestFile Create(string filePath)
        {
            return new TestFile(filePath, filePath);
        }

        public static TestFile Create(Assembly assembly, string relativeFilePath)
        {
            var uri = new UriBuilder(assembly.CodeBase ?? throw new NullReferenceException("Assembly has no CodeBase"));
            return new TestFile(uri.Path, relativeFilePath);
        }
    }
}
