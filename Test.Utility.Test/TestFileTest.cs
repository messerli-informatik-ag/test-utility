using System;
using System.IO;
using System.Net;
using Xunit;

namespace Messerli.Test.Utility.Test
{
    public class TestFileTest
    {
        [Fact]
        public void Create()
        {
            var testFile = TestFile.Create(typeof(TestFileTest).Assembly, "");
            var uri = new UriBuilder(testFile.SourceFilePath);
            Assert.True(File.Exists(uri.Path));
        }
    }
}
