using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Messerli.Test.Utility
{
    internal static class ExcludedTypesUtility
    {
        public static ISet<Type> CollectExcludedTypes(MemberInfo testMethod)
        {
            var types = testMethod
                .GetCustomAttributes<ExcludedTypesAttribute>()
                .SelectMany(attr => attr.Types);
            return new HashSet<Type>(types);
        }
    }
}
