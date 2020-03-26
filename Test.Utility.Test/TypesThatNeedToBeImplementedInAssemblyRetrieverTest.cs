using System;
using System.Collections.Generic;
using System.Linq;
using TestAssembly;
using Xunit;
using Xunit.Abstractions;

namespace Messerli.Test.Utility.Test
{
    public class TypesThatNeedToBeImplementedInAssemblyRetrieverTest
    {
        private const string TestAssemblyName = "TestAssembly";

        private const string EmptyAssemblyName = "EmptyAssembly";

        private static readonly Type[] TypesThatNeedToBeImplementedInAssembly =
        {
            typeof(ImplementationFactory),
            typeof(AbstractClassWithMethod),
            typeof(AbstractClassWithProperty),
            typeof(IInterfaceWithMethod),
            typeof(IInterfaceWithProperty),
            typeof(IEmptyInterfaceInheritingMethods),
            typeof(EmptyAbstractClass),
        };

        private readonly ITestOutputHelper _testOutputHelper;

        public TypesThatNeedToBeImplementedInAssemblyRetrieverTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void FindsOnlyTypesThatNeedToBeImplemented()
        {
            var types = TypesThatNeedToBeImplementedInAssemblyRetriever.GetTypesThatNeedToBeImplementedInAssembly(TestAssemblyName);
            PrintTypes(types);
            Assert.Equal(OrderByFullName(TypesThatNeedToBeImplementedInAssembly), OrderByFullName(types));
        }

        [Fact]
        public void FindsAssemblyWhenCodeHasNoExplicitReferenceToAssembly()
        {
            Assert.Empty(TypesThatNeedToBeImplementedInAssemblyRetriever.GetTypesThatNeedToBeImplementedInAssembly(EmptyAssemblyName));
        }

        [Fact]
        public void ThrowsWhenAssemblyIsNotFound()
        {
            Assert.Throws<ArgumentException>(() =>
                TypesThatNeedToBeImplementedInAssemblyRetriever.GetTypesThatNeedToBeImplementedInAssembly("NonExistingAssembly"));
        }

        [Theory]
        [TypesThatNeedToBeImplementedInAssemblyData(TestAssemblyName)]
        public void AttributeSmokeTest(Type type)
        {
            Assert.Contains(type, TypesThatNeedToBeImplementedInAssembly);
        }

        private static IEnumerable<Type> OrderByFullName(IEnumerable<Type> types)
            => types.OrderBy(t => t.FullName);

        private void PrintTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                _testOutputHelper.WriteLine(type.FullName);
            }
        }
    }
}
