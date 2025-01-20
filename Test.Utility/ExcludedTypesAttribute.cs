using System;
using System.Collections.Generic;

namespace Messerli.Test.Utility;

/// <summary>
/// Blacklists a hardcoded list of types.
/// Used together with <see cref="TypesThatNeedToBeImplementedInAssemblyDataAttribute"/> and <see cref="TypesRegisteredInContainerRetriever"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ExcludedTypesAttribute(params Type[] types) : Attribute
{
    internal IEnumerable<Type> Types { get; } = types;
}
