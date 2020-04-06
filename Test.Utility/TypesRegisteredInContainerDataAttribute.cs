#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Autofac;
using Xunit.Sdk;
using static Messerli.Test.Utility.TypesRegisteredInContainerRetriever;

namespace Messerli.Test.Utility
{
    /// <summary>
    /// <para>
    ///     Provides all types registered in an Autofac <see cref="IContainer"/> as theory data.
    /// </para>
    /// </summary>
    /// <example>
    /// <code>
    /// using System;
    /// using Autofac;
    /// using Messerli.CompositionRoot;
    /// using Xunit;
    ///
    /// public sealed class FooModuleTest : IDisposable
    /// {
    ///     private readonly IContainer _container = CreateContainer();
    ///
    ///     public static IContainer CreateContainer() => new CompositionRootBuilder().Build();
    ///
    ///     [Theory]
    ///     [TypesRegisteredInContainerData(nameof(CreateContainer))]
    ///     public void TypesRegisteredInContainerCanBeResolved(Type type) => _container.Resolve(type);
    ///
    ///     public void Dispose() => _container.Dispose();
    /// }
    /// </code>
    /// </example>
    public sealed class TypesRegisteredInContainerDataAttribute : DataAttribute
    {
        private readonly string _createContainerMethodName;

        public TypesRegisteredInContainerDataAttribute(string createContainerMethodName)
        {
            _createContainerMethodName = createContainerMethodName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            try
            {
                return GetTypesRegisteredInContainer(testMethod).Result;
            }
            catch (AggregateException exception) when (exception.Flatten().InnerExceptions.Count == 1)
            {
                ExceptionDispatchInfo.Capture(exception.Flatten().InnerException).Throw();
                throw null!;
            }
        }

        private async Task<IEnumerable<object[]>> GetTypesRegisteredInContainer(MemberInfo testMethod)
        {
            var targetType = testMethod.DeclaringType ?? throw new NullReferenceException();
            var types = await GetTypesRegisteredInContainerViaMethod(targetType, _createContainerMethodName);
            return types.Select(type => new[] { type });
        }
    }
}
