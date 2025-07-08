using Bazario.AspNetCore.Shared.ArchitectureTests.Application;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests.Application
{
    public sealed class DomainEventHandlersTests
        : DomainEventHandlersTestsBase
    {
        public DomainEventHandlersTests()
        {
            SetTestAssembly(
                TestAssembliesStorage.TestAssemblies.ApplicationAssembly);
        }
    }
}
