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
            Assert.Equal(TypesThatNeedToBeImplementedInAssembly.OrderByFullTypeName(), types.OrderByFullTypeName());
        }

        [Fact]
        public void FindsAssemblyWhenCodeHasNoExplicitReferenceToAssembly()
        {
            Assert.Empty(TypesThatNeedToBeImplementedInAssemblyRetriever.GetTypesThatNeedToBeImplementedInAssembly(EmptyAssemblyName));
        }

        [Fact]
        public void ThrowsWhenAssemblyIsNotFound()
        {
            Assert.Throws<InvalidOperationException>(() =>
                TypesThatNeedToBeImplementedInAssemblyRetriever.GetTypesThatNeedToBeImplementedInAssembly("NonExistingAssembly"));
        }

        [Theory]
        [TypesThatNeedToBeImplementedInAssemblyData(TestAssemblyName)]
        [ExcludedTypes(typeof(IInterfaceWithMethod), typeof(ImplementationFactory))]
        [ExcludedTypes(typeof(AbstractClassWithProperty))]
        public void AttributeSmokeTest(Type type)
        {
            Assert.Contains(type, TypesThatNeedToBeImplementedInAssembly);
        }

        private void PrintTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                _testOutputHelper.WriteLine(type.FullName);
            }
        }
    }
}
