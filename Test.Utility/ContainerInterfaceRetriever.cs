using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace Messerli.Test.Utility
{
    public static class ContainerInterfaceRetriever
    {
        private static readonly IEnumerable<string> ExcludedRootNamespaces =
            new[] { nameof(System), "Windows", "Internal", nameof(Autofac) };

        public static IEnumerable<Type> GetAssemblyInterfaces(IContainer container)
            => container
                .ComponentRegistry
                .Registrations
                .Select(registration => registration.Activator.LimitType.Assembly)
                .SelectMany(GetAssemblyTypes)
                .ToHashSet();

        private static IEnumerable<Type> GetAssemblyTypes(Assembly assembly)
            => assembly
                .GetTypes()
                .Where(IsValidType);

        private static bool IsValidType(Type type)
            => type.IsInterface &&
               !type.IsGenericType &&
               !type.BelongsToAnyNamespace(ExcludedRootNamespaces);

        private static bool BelongsToAnyNamespace(this Type type, IEnumerable<string> namespaces)
            => namespaces.Any(namespaceName =>  NamespaceIsChildOf(type, namespaceName));

        private static bool NamespaceIsChildOf(Type type, string @string)
            => GetNamespaceRoot(type) == @string;

        private static string GetNamespaceRoot(Type type)
            => type.Namespace?.Split('.').FirstOrDefault()
               ?? throw new InvalidOperationException("No namespace information present");
    }
}
