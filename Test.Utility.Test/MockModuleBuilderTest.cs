using Autofac;
using Autofac.Core;
using Moq;
using Xunit;
using Module = Autofac.Module;

namespace Messerli.Test.Utility.Test
{
    public class MockModuleBuilderTest
    {
        [Fact]
        public void EmptyModuleBuilds()
        {
            Assert.NotNull(new MockModuleBuilder().Build());
        }

        [Fact]
        public void ModuleBuildsWithRegisteredModules()
        {
            var container = BuildModule(
                new MockModuleBuilder()
                    .RegisterModule(new FooModule())
                    .RegisterModule<BarModule>()
                    .Build());

            Assert.NotNull(container.Resolve<IFoo>());
            Assert.NotNull(container.Resolve<IBar>());
        }

        [Fact]
        public void ModuleBuildsWithRegisteredMocks()
        {
            var fooMock = new Mock<IFoo>();

            var container = BuildModule(
                new MockModuleBuilder()
                    .RegisterMockFor(fooMock)
                    .RegisterMockFor<IBar>()
                    .Build());

            Assert.NotNull(container.Resolve<IFoo>());
            Assert.NotNull(container.Resolve<IBar>());
        }

        [Fact]
        public void ModuleBuildsWithRegisteredInstance()
        {
            var container = BuildModule(
                new MockModuleBuilder()
                    .RegisterInstance(new Foo())
                    .RegisterInstance(new Bar())
                    .Build());

            Assert.NotNull(container.Resolve<Foo>());
            Assert.NotNull(container.Resolve<Bar>());
        }

        [Fact]
        public void ModuleBuildsWithRegisteredRegistrationActions()
        {
            var container = BuildModule(
                new MockModuleBuilder()
                    .Register(builder => builder.RegisterType<Foo>().As<IFoo>())
                    .Register(builder => builder.RegisterType<Bar>().As<IBar>())
                    .Build());

            Assert.NotNull(container.Resolve<IFoo>());
            Assert.NotNull(container.Resolve<IBar>());
        }

        private static IContainer BuildModule(IModule module)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            return builder.Build();
        }

        public interface IFoo
        {
        }

        public interface IBar
        {
        }

        private sealed class Foo : IFoo
        {
        }

        private sealed class Bar : IBar
        {
        }

        private sealed class FooModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<Foo>().As<IFoo>();
            }
        }

        private sealed class BarModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<Bar>().As<IBar>();
            }
        }
    }
}