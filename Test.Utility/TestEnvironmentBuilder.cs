using System.Collections.Immutable;

namespace Messerli.Test.Utility;

public sealed class TestEnvironmentBuilder
{
    private readonly ImmutableList<TestFile> _testFiles = [];

    public TestEnvironmentBuilder()
    {
    }

    private TestEnvironmentBuilder(ImmutableList<TestFile> testFiles)
        => _testFiles = testFiles;

    public TestEnvironmentBuilder AddTestFile(TestFile file)
        => new TestEnvironmentBuilder(_testFiles.Add(file));

    public TestEnvironmentProvider Build()
        => new TestEnvironmentProvider(_testFiles);
}
