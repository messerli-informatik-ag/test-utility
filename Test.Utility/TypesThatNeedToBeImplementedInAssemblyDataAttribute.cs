using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;
using static Messerli.Test.Utility.ExcludedTypesUtility;

namespace Messerli.Test.Utility;

/// <summary>
/// <para>
///     Provides all types that need to be implemented in an assembly as theory data.
///     You can use <see cref="ExcludedTypesAttribute" /> to exclude types.
/// </para>
/// <para>
///     The following types need to be implemented (excluding generic types):
///     <list type="bullet">
///         <item><description>delegates</description></item>
///         <item><description>abstract classes</description></item>
///         <item><description>non-empty interfaces</description></item>
///     </list>
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
///     private readonly IContainer _container = new CompositionRootBuilder().Build();
///
///     [Theory]
///     [TypesThatNeedToBeImplementedInAssemblyData("AssemblyToSearch")]
///     public void AbstractTypesCanBeResolved(Type type) => _container.Resolve(type);
///
///     public void Dispose() => _container.Dispose();
/// }
/// </code>
/// </example>
public sealed class TypesThatNeedToBeImplementedInAssemblyDataAttribute(string assemblyName) : DataAttribute
{
    private readonly string _assemblyName = assemblyName;

    public bool IncludeInternal { get; set; } = false;

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        => TypesThatNeedToBeImplementedInAssemblyRetriever
            .GetTypesThatNeedToBeImplementedInAssembly(_assemblyName, IncludeInternal)
            .Except(CollectExcludedTypes(testMethod))
            .Select(type => new object[] { type });
}
