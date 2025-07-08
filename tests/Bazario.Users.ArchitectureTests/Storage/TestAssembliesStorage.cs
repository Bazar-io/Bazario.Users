using Bazario.AspNetCore.Shared.ArchitectureTests.CleanArchitecture;

namespace Bazario.Users.ArchitectureTests.Storage
{
    internal static class TestAssembliesStorage
    {
        public static TestBaseAsseblies TestAssemblies =
            new TestBaseAsseblies(
                domainAssembly: typeof(Users.Domain.AssemblyMarker).Assembly,
                applicationAssembly: typeof(Users.Application.AssemblyMarker).Assembly,
                infrastructureAssembly: typeof(Users.Infrastructure.AssemblyMarker).Assembly,
                presentationAssembly: typeof(Users.WebAPI.AssemblyMarker).Assembly
            );
    }
}
