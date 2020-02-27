#if NETSTANDARD2_0
using System.Collections.Generic;

namespace Messerli.Test.Utility
{
    internal static class EnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
            => new HashSet<T>(enumerable);
    }
}
#endif
