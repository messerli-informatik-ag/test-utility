using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Messerli.CompositionRoot;
using Xunit;

namespace Messerli.Test.Utility.Test;

public class TypesRegisteredInContainerDataRetrieverTest
{
    private static readonly ISet<Type> TypesRegisteredInContainer = new HashSet<Type>
    {
        typeof(IFoo),
        typeof(IBar),
    };

    private interface IFoo;

    private interface IBar;

    [Fact]
    public async Task RetrievesTypesRegisteredInContainer()
    {
        var types = await GetTypesRegisteredInContainerViaMethod(nameof(CreateContainer));
        Assert.Equal(TypesRegisteredInContainer.OrderByFullTypeName(), types.OrderByFullTypeName());
    }

    [Fact]
    public async Task WorksWithContainerThatNeedsToBeDisposedAsynchronously()
    {
        var expectedTypes = new[] { typeof(AsyncDisposable) };
        var types = await GetTypesRegisteredInContainerViaMethod(nameof(CreateContainerWithAsyncDisposableObject));
        Assert.Equal(expectedTypes, types);
    }

    [Fact]
    public async Task ThrowsWhenCreateContainerMethodIsNotStatic()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await GetTypesRegisteredInContainerViaMethod(nameof(CreateContainerNonStatic));
        });
    }

    [Fact]
    public async Task ThrowsWhenCreateContainerMethodIsPrivate()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await GetTypesRegisteredInContainerViaMethod(nameof(CreateContainerPrivate));
        });
    }

    [Fact]
    public async Task ThrowsWhenCreateContainerHasIncorrectReturnType()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await GetTypesRegisteredInContainerViaMethod(nameof(CreateContainerWithIncorrectReturnType));
        });
    }

    [Fact]
    public async Task ThrowsWhenCreateContainerHasParametersType()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await GetTypesRegisteredInContainerViaMethod(nameof(CreateContainerWithParameters));
        });
    }

    [Theory]
    [TypesRegisteredInContainerData(nameof(CreateContainer))]
    [ExcludedTypes(typeof(IFoo))]
    public void AttributeSmokeTest(Type type)
    {
        var excludedTypes = new[] { typeof(IFoo) };
        Assert.Contains(type, TypesRegisteredInContainer.Except(excludedTypes));
    }

    public static IContainer CreateContainer()
        => new CompositionRootBuilder()
            .RegisterModule<TestModule>()
            .Build();

    public static IContainer CreateContainerWithAsyncDisposableObject()
        => new CompositionRootBuilder()
            .RegisterModule(new ModuleBuilder()
                .RegisterInstance(new AsyncDisposable())
                .Build())
            .Build();

    public static string CreateContainerWithIncorrectReturnType()
        => string.Empty;

    public static IContainer CreateContainerWithParameters(string foo)
        => new CompositionRootBuilder().Build();

    public IContainer CreateContainerNonStatic()
        => new CompositionRootBuilder().Build();

    private static IContainer CreateContainerPrivate()
        => new CompositionRootBuilder().Build();

    private static Task<IEnumerable<Type>> GetTypesRegisteredInContainerViaMethod(string createContainerMethodName)
        => TypesRegisteredInContainerRetriever.GetTypesRegisteredInContainerViaMethod(
            typeof(TypesRegisteredInContainerDataRetrieverTest),
            createContainerMethodName);

    private class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Foo>().As<IFoo>();
            builder.RegisterType<Bar>().As<IBar>();
        }
    }

    private class Foo : IFoo;

    private class Bar : IBar;

    private class AsyncDisposable : IAsyncDisposable
    {
        public ValueTask DisposeAsync() => default;
    }
}
