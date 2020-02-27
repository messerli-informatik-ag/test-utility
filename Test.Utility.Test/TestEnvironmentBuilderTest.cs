using System.IO;
using Xunit;

namespace Messerli.Test.Utility.Test
{
    public sealed class TestEnvironmentBuilderTest
    {
        [Fact]
        public void CreateTestEnvironmentBuilder()
        {
            var testFile = TestFile.Create("file1.txt");
            using var testEnvironmentProvider = new TestEnvironmentBuilder()
                .AddTestFile(testFile)
                .Build();

            var tempPath = Path.GetTempPath();
            var path = Path.Combine(tempPath, testEnvironmentProvider.RootDirectory, testFile.RelativeFilePath);

            Assert.True(File.Exists(path));
        }
    }
}
