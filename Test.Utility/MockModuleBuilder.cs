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

        public MockModuleBuilder RegisterMockFor<TInterface>() where TInterface : class
        {
            _mockRegistrations.Add(
                builder => builder.Register(context => new Mock<TInterface>().Object).As<TInterface>());

            return this;
        }

        public MockModuleBuilder RegisterMockFor<TInterface>(Mock<TInterface> mock) where TInterface : class
        {
            _mockRegistrations.Add(
                builder => builder.Register(context => mock.Object).As<TInterface>());

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