using Autofac;
using Messerli.CompositionRoot;
using Xunit;

namespace Messerli.Test.Utility.Test
{
    public class ComponentContextExtensionTest
    {
        private delegate IFoo CreateFoo();

        private interface IFoo
        {
        }

        [Fact]
        public void ReturnsAllRegisteredTypes()
        {
            var expectedTypes = new[]
            {
                typeof(IFoo),
                typeof(CreateFoo),
            };
            using var container = new CompositionRootBuilder()
                .RegisterModule<FooModule>()
                .Build();
            Assert.Equal(expectedTypes.OrderByFullTypeName(), container.GetRegisteredTypes().OrderByFullTypeName());
        }

        [Fact]
        public void KeyedServicesAreIgnored()
        {
            using var container = new CompositionRootBuilder()
                .RegisterModule<ModuleWithKeyedServices>()
                .Build();
            Assert.Empty(container.GetRegisteredTypes());
        }

        private class FooModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<Foo>().As<IFoo>();
                builder.RegisterInstance<CreateFoo>(() => new Foo());
            }
        }

        private class ModuleWithKeyedServices : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<Foo>().Named<IFoo>("foo");
                builder.RegisterType<Foo>().Keyed<IFoo>(42);
            }
        }

        private class Foo : IFoo
        {
        }
    }
}
