using System.Collections.Generic;
using Autofac;

namespace Messerli.Test.Utility
{
    public class MockModule : Module
    {
        public delegate void Register(ContainerBuilder builder);

        public MockModule(List<Register> mockRegistrations)
        {
            _mockRegistrations = mockRegistrations;
        }

        private readonly List<Register> _mockRegistrations;

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var registration in _mockRegistrations)
            {
                registration(builder);
            }
        }
    }
}