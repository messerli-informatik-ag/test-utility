using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Moq;

namespace Messerli.Test.Utility
{
    public class MockModuleBuilder
    {
        private readonly List<MockModule.Register> _mockRegistrations = new List<MockModule.Register>();

        public MockModuleBuilder RegisterMockFor<TInterface>()
            where TInterface : class
            => Register(builder => builder.Register(context => new Mock<TInterface>().Object).As<TInterface>());

        public MockModuleBuilder RegisterMockFor<TInterface>(Mock<TInterface> mock)
            where TInterface : class
            => Register(builder => builder.Register(context => mock.Object).As<TInterface>());

        public MockModuleBuilder RegisterModule<TModule>()
            where TModule : IModule, new()
            => Register(builder => builder.RegisterModule<TModule>());

        public MockModuleBuilder RegisterModule<TModule>(TModule module)
            where TModule : IModule
            => Register(builder => builder.RegisterModule(module));

        public MockModuleBuilder RegisterInstance<TInstance>(TInstance instance)
            where TInstance : class
            => Register(builder => builder.RegisterInstance(instance));

        public MockModuleBuilder Register(MockModule.Register registrationFunction)
        {
            _mockRegistrations.Add(registrationFunction);
            return this;
        }

        public MockModuleBuilder MockEnvironmentVariable(string variable, string value)
        {
            Environment.SetEnvironmentVariable(variable, value);

            return this;
        }

        public IModule Build()
        {
            return new MockModule(_mockRegistrations);
        }
    }
}
