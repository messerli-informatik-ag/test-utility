using System;
using System.Collections.Generic;
using System.Linq;

namespace Messerli.Test.Utility.Test
{
    internal static class EnumerableExtension
    {
        public static IEnumerable<Type> OrderByFullTypeName(this IEnumerable<Type> types)
            => types.OrderBy(t => t.FullName);
    }
}
