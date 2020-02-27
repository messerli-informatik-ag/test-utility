using System.Collections.Immutable;

namespace Messerli.Test.Utility
{
    public sealed class TestEnvironmentBuilder
    {
        private readonly ImmutableList<TestFile> _testFiles = ImmutableList.Create<TestFile>();

        public TestEnvironmentBuilder()
        {
        }

        private TestEnvironmentBuilder(ImmutableList<TestFile> testFiles) =>
            _testFiles = testFiles;

        public TestEnvironmentBuilder AddTestFile(TestFile file)
        {
            return new TestEnvironmentBuilder(_testFiles.Add(file));
        }

        public TestEnvironmentProvider Build()
        {
            return new TestEnvironmentProvider(_testFiles);
        }
    }
}
