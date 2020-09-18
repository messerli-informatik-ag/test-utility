using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace Messerli.Test.Utility
{
    public static class ComponentContextExtension
    {
        private static readonly Type[] BlacklistedTypes =
        {
            typeof(IComponentContext),
            typeof(ILifetimeScope),
        };

        public static IEnumerable<Type> GetRegisteredTypes(this IComponentContext componentContext)
            => GetRegistrations(componentContext)
                .SelectMany(registration => registration.Services)
                .Where(IsTypedService)
                .OfType<IServiceWithType>()
                .Select(service => service.ServiceType)
                .Where(IsTypeNotOnBlacklist);

        private static bool IsTypedService(Service service) => service is TypedService;

        private static bool IsTypeNotOnBlacklist(Type type) => !BlacklistedTypes.Contains(type);

        private static IEnumerable<IComponentRegistration> GetRegistrations(IComponentContext componentContext)
            => componentContext.ComponentRegistry.Registrations;
    }
}
