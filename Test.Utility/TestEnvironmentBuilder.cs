using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.Core;
using Messerli.Test.Utility;

namespace Messerli.Test.Utility
{
    public class TestEnvironmentBuilder
    {
        private readonly List<TestFile> _testFiles = new List<TestFile>();

        public TestEnvironmentBuilder AddTestFile(TestFile file)
        {
            _testFiles.Add(file);
            return this;
        }

        public TestEnvironmentProvider Build()
        {
            return new TestEnvironmentProvider(_testFiles);
        }
    }
}
