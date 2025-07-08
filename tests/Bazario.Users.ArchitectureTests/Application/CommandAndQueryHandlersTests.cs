using Bazario.AspNetCore.Shared.ArchitectureTests.Application;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests.Application
{
    public sealed class CommandAndQueryHandlersTests
        : CommandAndQueryHandlersTestsBase
    {
        public CommandAndQueryHandlersTests()
        {
            SetTestAssembly(
                TestAssembliesStorage.TestAssemblies.ApplicationAssembly);
        }
    }
}
