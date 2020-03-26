using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Messerli.Test.Utility
{
    /// <summary>
    /// <para>
    ///     Provides all types that need to be implemented in an assembly as theory data.
    /// </para>
    /// <para>
    ///     The following types need to be implemented:
    ///     <list type="bullet">
    ///         <item><description>delegates</description></item>
    ///         <item><description>abstract classes</description></item>
    ///         <item><description>non-empty interfaces</description></item>
    ///     </list>
    /// </para>
    /// </summary>
    public class TypesThatNeedToBeImplementedInAssemblyDataAttribute : DataAttribute
    {
        private readonly string _assemblyName;

        public TypesThatNeedToBeImplementedInAssemblyDataAttribute(string assemblyName)
        {
            _assemblyName = assemblyName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
            => TypesThatNeedToBeImplementedInAssemblyRetriever
                .GetTypesThatNeedToBeImplementedInAssembly(_assemblyName)
                .Select(type => new object[] { type });
    }
}
