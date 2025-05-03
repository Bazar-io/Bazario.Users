# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5003
EXPOSE 5004


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bazario.Users/src/Bazario.Users.WebAPI/Bazario.Users.WebAPI.csproj", "Bazario.Users.WebAPI/"]
COPY ["Bazario.Users/src/Bazario.Users.Application/Bazario.Users.Application.csproj", "Bazario.Users.Application/"]
COPY ["Bazario.Users/src/Bazario.Users.Domain/Bazario.Users.Domain.csproj", "Bazario.Users.Domain/"]
COPY ["Bazario.Users/src/Bazario.Users.Infrastructure/Bazario.Users.Infrastructure.csproj", "Bazario.Users.Infrastructure/"]

COPY Bazario.AspNetCore.Shared/NuGetPackages /nuget-packages
RUN dotnet nuget add source /nuget-packages --name NuGetPackages

COPY Bazario.Docker/scripts/wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

RUN dotnet restore "./Bazario.Users.WebAPI/Bazario.Users.WebAPI.csproj"
COPY Bazario.Users/src/ .
WORKDIR "/src/Bazario.Users.WebAPI"
RUN dotnet build "./Bazario.Users.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Bazario.Users.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bazario.Users.WebAPI.dll"]