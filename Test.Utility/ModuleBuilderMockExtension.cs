using Messerli.CompositionRoot;
using Moq;

namespace Messerli.Test.Utility
{
    public static class ModuleBuilderMockExtension
    {
        public static void RegisterMock<T>(this ModuleBuilder moduleBuilder)
            where T : class
            => moduleBuilder.Register(builder => builder.RegisterMock<T>());

        public static void RegisterMock<T>(this ModuleBuilder moduleBuilder, Mock<T> mock)
            where T : class
            => moduleBuilder.Register(builder => builder.RegisterMock(mock));
    }
}
