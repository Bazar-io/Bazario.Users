using Bazario.AspNetCore.Shared.ArchitectureTests.Infrastructure;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests.Infrastructure
{
    public sealed class EventConsumersTests : EventConsumersTestsBase
    {
        public EventConsumersTests()
        {
            SetTestAssembly(
                TestAssembliesStorage.TestAssemblies.InfrastructureAssembly);
        }
    }
}
