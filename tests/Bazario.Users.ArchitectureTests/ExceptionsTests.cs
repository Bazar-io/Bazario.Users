using Bazario.AspNetCore.Shared.ArchitectureTests;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests
{
    public sealed class ExceptionsTests : ExceptionsTestsBase
    {
        public ExceptionsTests()
        {
            SetTestAssemblies(
                TestAssembliesStorage.TestAssemblies.ApplicationAssembly,
                TestAssembliesStorage.TestAssemblies.InfrastructureAssembly,
                TestAssembliesStorage.TestAssemblies.PresentationAssembly
            );
        }
    }
}
