using Bazario.AspNetCore.Shared.ArchitectureTests.Domain;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests.Domain
{
    public sealed class DomainEventsTests : DomainEventsTestsBase
    {
        public DomainEventsTests()
        {
            SetTestAssembly(
                TestAssembliesStorage.TestAssemblies.DomainAssembly);
        }
    }
}
