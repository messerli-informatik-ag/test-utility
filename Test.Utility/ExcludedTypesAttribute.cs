using System;
using System.Collections.Generic;

namespace Messerli.Test.Utility
{
    /// <summary>
    /// Blacklists a hardcoded list of types. Used together with <see cref="TypesThatNeedToBeImplementedInAssemblyDataAttribute"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ExcludedTypesAttribute : Attribute
    {
        public ExcludedTypesAttribute(params Type[] types)
        {
            Types = types;
        }

        internal IEnumerable<Type> Types { get; }
    }
}
