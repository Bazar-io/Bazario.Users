using Bazario.AspNetCore.Shared.ArchitectureTests.Application;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests.Application
{
    public sealed class ValidatorsTests : ValidatorsTestsBase
    {
        public ValidatorsTests()
        {
            SetTestAssembly(
                TestAssembliesStorage.TestAssemblies.ApplicationAssembly);
        }
    }
}
