using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;

namespace Messerli.Test.Utility
{
    internal static class TypesRegisteredInContainerRetriever
    {
        public static async Task<IEnumerable<Type>> GetTypesRegisteredInContainerViaMethod(IReflect targetType, string createContainerMethodName)
        {
            await using var container = CreateContainer(targetType, createContainerMethodName);
            return container.GetRegisteredTypes();
        }

        private static IContainer CreateContainer(IReflect targetType, string createContainerMethodName)
        {
            var createContainerMethod = GetCreateContainerMethod(targetType, createContainerMethodName);
            return (IContainer?)createContainerMethod.Invoke(null, null)
                   ?? throw new NullReferenceException();
        }

        private static MethodInfo GetCreateContainerMethod(IReflect type, string createContainerMethodName)
        {
            var createContainerMethod = type.GetMethod(createContainerMethodName, BindingFlags.Static | BindingFlags.Public)
                ?? throw new InvalidOperationException("Unable to access container creation method. Make sure it's static and public.");
            ValidateCreateContainerParameters(createContainerMethod);
            ValidateCreateContainerReturnType(createContainerMethod);
            return createContainerMethod;
        }

        private static void ValidateCreateContainerParameters(MethodInfo method)
        {
            if (method.GetParameters().Any())
            {
                throw new InvalidOperationException("The container creation method must be nullary.");
            }
        }

        private static void ValidateCreateContainerReturnType(MethodInfo method)
        {
            var expectedReturnType = typeof(IContainer);
            var returnType = method.ReturnType;
            if (!expectedReturnType.IsAssignableFrom(returnType))
            {
                throw new InvalidOperationException(
                    $"The return type of the container creation method should be '{expectedReturnType.Name}' but was '{returnType}'");
            }
        }
    }
}
