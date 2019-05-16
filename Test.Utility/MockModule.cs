using System.Collections.Generic;
using Autofac;

namespace Messerli.Test.Utility
{
    public class MockModule : Module
    {
        public delegate void Register(ContainerBuilder builder);

        private readonly List<Register> _mockRegistrations;

        public MockModule(List<Register> mockRegistrations)
        {
            _mockRegistrations = mockRegistrations;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var registration in _mockRegistrations)
            {
                registration(builder);
            }
        }
    }
}
