using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace Messerli.Test.Utility
{
    public static class ContainerInterfaceRetriever
    {
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
                .Where(type => type.IsInterface && !type.IsGenericType && !BelongsToAutofacNamespace(type));

        private static bool BelongsToAutofacNamespace(Type type)
            => NamespaceContains(type, nameof(Autofac));

        private static bool NamespaceContains(Type type, string @string)
            => type.Namespace?.Contains(@string) ?? false;
    }
}
