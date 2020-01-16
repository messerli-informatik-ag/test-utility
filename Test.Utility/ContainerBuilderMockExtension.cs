using Autofac;
using Autofac.Builder;
using Moq;

namespace Messerli.Test.Utility
{
    public static class ContainerBuilderMockExtension
    {
        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterMock<T>(this ContainerBuilder builder)
            where T : class
            => builder.Register(context => new Mock<T>().Object).As<T>();

        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterMock<T>(this ContainerBuilder builder, Mock<T> mock)
            where T : class
            => builder.Register(context => mock.Object).As<T>();
    }
}
