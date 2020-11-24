using System;
using System.Collections.Generic;
using System.IO;
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
            => GetAssemblyByName(assemblyName)
                .GetTypes()
                .Where(IsImplementableType)
                .Where(IsNonGenericType);

        private static Assembly GetAssemblyByName(string assemblyName)
        {
            try
            {
                return GetAssemblyFromLoadedAssemblies(assemblyName) ?? Assembly.Load(assemblyName);
            }
            catch (Exception exception) when (exception is BadImageFormatException or IOException)
            {
                throw new InvalidOperationException($"Unable to load assembly '{assemblyName}'", exception);
            }
        }

        private static Assembly? GetAssemblyFromLoadedAssemblies(string assemblyName)
            => AppDomain.CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == assemblyName);

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
