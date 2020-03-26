﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Messerli.Test.Utility
{
    internal static class TypesThatNeedToBeImplementedInAssemblyRetriever
    {
        public static IEnumerable<Type> GetTypesThatNeedToBeImplementedInAssembly(string assemblyName)
            => GetAssemblyFromLoadedAssemblies(assemblyName)
                .GetTypes()
                .Where(IsImplementableType);

        private static Assembly GetAssemblyFromLoadedAssemblies(string assemblyName)
            => AppDomain.CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == assemblyName)
                    ?? throw new ArgumentException($"Assembly '{assemblyName}' is not loaded or does not exist", nameof(assemblyName));

        private static bool IsImplementableType(Type type)
            => IsDelegate(type) || IsImplementableInterface(type) || IsAbstractClass(type);

        private static bool IsDelegate(Type type)
            => typeof(Delegate).IsAssignableFrom(type);

        private static bool IsImplementableInterface(Type type)
            => type.IsInterface && !IsMarkerType(type);

        private static bool IsAbstractClass(Type type)
            => type.IsAbstract && type.IsClass;

        private static bool IsMarkerType(Type type)
            => !type.GetMethods().Any() && !type.GetProperties().Any() && type.GetInterfaces().All(IsMarkerType);
    }
}