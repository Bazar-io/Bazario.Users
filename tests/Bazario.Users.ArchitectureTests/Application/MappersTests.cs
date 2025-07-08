using Bazario.AspNetCore.Shared.ArchitectureTests.Application;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests.Application
{
    public sealed class MappersTests : MappersTestsBase
    {
        public MappersTests()
        {
            SetTestAssembly(
                TestAssembliesStorage.TestAssemblies.ApplicationAssembly);
        }
    }
}
