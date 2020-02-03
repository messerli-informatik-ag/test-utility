using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace Messerli.Test.Utility
{
    public static class ContainerInterfaceRetriever
    {
        private static readonly IEnumerable<string> ExcludedTypes =
            new[] { nameof(Autofac), nameof(System) };
        
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
               !type.BelongsToAnyNamespace(ExcludedTypes);

        private static bool BelongsToAnyNamespace(this Type type, IEnumerable<string> namespaces)
            => namespaces.Any(namespaceName =>  NamespaceContains(type, namespaceName));

        private static bool NamespaceContains(Type type, string @string)
            => type.Namespace?.Contains(@string) ?? false;
    }
}
