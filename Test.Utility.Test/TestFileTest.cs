using System.IO;
using Xunit;

namespace Messerli.Test.Utility.Test
{
    public sealed class TestFileTest
    {
        [Fact]
        public void Create()
        {
            var testFile = TestFile.Create(typeof(TestFileTest).Assembly, string.Empty);
            Assert.True(File.Exists(testFile.SourceFilePath));
        }
    }
}
