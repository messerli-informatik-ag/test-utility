#pragma warning disable 618

using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using Messerli.CompositionRoot;
using Messerli.Test.Utility.Test.Stubs;
using Xunit;

namespace Messerli.Test.Utility.Test
{
    public sealed class ContainerInterfaceRetrieverTest
    {
        [Theory]
        [MemberData(nameof(GetTestModules))]
        public void ReturnsExpectedInterfaces(IEnumerable<Type> expected, IModule module)
        {
            using var container = new CompositionRootBuilder()
                .RegisterModule(module)
                .Build();

            Assert.Equal(expected, ContainerInterfaceRetriever.GetAssemblyInterfaces(container).ToArray());
        }

        public static TheoryData<IEnumerable<Type>, IModule> GetTestModules()
            => new TheoryData<IEnumerable<Type>, IModule>
            {
                { new[] { typeof(ITestInterface), typeof(ITestInterface2) }, new ModuleBuilder().RegisterMock<ITestInterface>().Build() },
                { new Type[0], new ModuleBuilder().RegisterMock<IEnumerable<string>>().Build() },
                { new Type[0], new ModuleBuilder().RegisterMock<IModule>().Build() },
            };
    }
}
