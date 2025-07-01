# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Nakliye360.API/Nakliye360.API.csproj", "src/Nakliye360.API/"]
COPY ["src/Nakliye360.Infrastructure/Nakliye360.Infrastructure.csproj", "src/Nakliye360.Infrastructure/"]
COPY ["src/Nakliye360.Application/Nakliye360.Application.csproj", "src/Nakliye360.Application/"]
COPY ["src/Nakliye360.Domain/Nakliye360.Domain.csproj", "src/Nakliye360.Domain/"]
COPY ["src/Nakliye360.Persistence/Nakliye360.Persistence.csproj", "src/Nakliye360.Persistence/"]
RUN dotnet restore "./src/Nakliye360.API/Nakliye360.API.csproj"
COPY . .
WORKDIR "/src/src/Nakliye360.API"
RUN dotnet build "./Nakliye360.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Nakliye360.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nakliye360.API.dll"]