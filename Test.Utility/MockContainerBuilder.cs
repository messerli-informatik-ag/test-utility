using Autofac;
using Moq;

namespace Messerli.Test.Utility
{
    public static class MockContainerBuilder
    {
        public static void RegisterMock<T>(this ContainerBuilder builder)
            where T : class
            => builder.Register(context => new Mock<T>().Object).As<T>();

        public static void RegisterMock<T>(this ContainerBuilder builder, Mock<T> mock)
            where T : class
            => builder.Register(context => mock.Object).As<T>();
    }
}
