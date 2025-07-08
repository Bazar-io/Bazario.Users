using Bazario.AspNetCore.Shared.ArchitectureTests.CleanArchitecture;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests
{
    public sealed class LayersTests : LayersTestsBase
    {
        public LayersTests()
        {
            SetTestAssemblies(TestAssembliesStorage.TestAssemblies);
        }
    }
}
