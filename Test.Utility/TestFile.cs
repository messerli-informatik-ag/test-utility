using System;
using System.Reflection;

namespace Messerli.Test.Utility
{
    public class TestFile
    {
        public string SourceFilePath { get; }

        public string RelativeFilePath { get; }

        public TestFile(string sourceFilePath, string relativeFilePath)
        {
            SourceFilePath = sourceFilePath;
            RelativeFilePath = relativeFilePath;
        }

        public static TestFile Create(string filePath)
        {
            return new TestFile(filePath, filePath);
        }

        public static TestFile Create(Assembly assembly, string relativeFilePath)
        {
            return new TestFile(assembly.CodeBase, relativeFilePath);
        }
    }
}