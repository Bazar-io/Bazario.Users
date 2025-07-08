using Bazario.AspNetCore.Shared.ArchitectureTests.Application;
using Bazario.Users.ArchitectureTests.Storage;

namespace Bazario.Users.ArchitectureTests.Application
{
    public sealed class CommandsAndQueriesTests
        : CommandsAndQueriesTestsBase
    {
        public CommandsAndQueriesTests()
        {
            SetTestAssembly(
                TestAssembliesStorage.TestAssemblies.ApplicationAssembly);
        }
    }
}
