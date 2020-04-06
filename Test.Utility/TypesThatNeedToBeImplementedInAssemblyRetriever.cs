using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Messerli.Test.Utility
{
    internal static class TypesThatNeedToBeImplementedInAssemblyRetriever
    {
        /// <exception cref="InvalidOperationException">Thrown when the assembly is not loaded or invalid.</exception>
        // Note to future developer: When throwing new exception types make sure that they are displayed by the test explorer.
        // Exceptions that don't get displayed properly are: ArgumentException
        public static IEnumerable<Type> GetTypesThatNeedToBeImplementedInAssembly(string assemblyName)
            => GetAssemblyFromLoadedAssemblies(assemblyName)
                .GetTypes()
                .Where(IsImplementableType)
                .Where(IsNonGenericType);

        private static Assembly GetAssemblyFromLoadedAssemblies(string assemblyName)
            => AppDomain.CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == assemblyName)
                    ?? throw new InvalidOperationException($"Assembly '{assemblyName}' is not loaded or does not exist");

        private static bool IsImplementableType(Type type)
            => IsDelegate(type) || IsImplementableInterface(type) || IsAbstractClass(type);

        private static bool IsNonGenericType(Type type)
            => !type.IsGenericType;

        private static bool IsDelegate(Type type)
            => typeof(Delegate).IsAssignableFrom(type);

        private static bool IsImplementableInterface(Type type)
            => type.IsInterface && !IsMarkerType(type);

        // Note that static classes are abstract sealed in IL.
        private static bool IsAbstractClass(Type type)
            => type.IsAbstract && type.IsClass && !type.IsSealed;

        private static bool IsMarkerType(Type type)
            => !type.GetMethods().Any() && !type.GetProperties().Any() && type.GetInterfaces().All(IsMarkerType);
    }
}
