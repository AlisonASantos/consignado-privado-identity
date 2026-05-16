
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ConsignadoPrivado.Identity.WebApi/ConsignadoPrivado.Identity.WebApi.csproj", "src/ConsignadoPrivado.Identity.WebApi/"]
COPY ["src/ConsignadoPrivado.Identity.Application/ConsignadoPrivado.Identity.Application.csproj", "src/ConsignadoPrivado.Identity.Application/"]
COPY ["src/ConsignadoPrivado.Common/ConsignadoPrivado.Common.csproj", "src/ConsignadoPrivado.Common/"]
COPY ["src/ConsignadoPrivado.Identity.Domain/ConsignadoPrivado.Identity.Domain.csproj", "src/ConsignadoPrivado.Identity.Domain/"]
COPY ["src/ConsignadoPrivado.Identity.IoC/ConsignadoPrivado.Identity.IoC.csproj", "src/ConsignadoPrivado.Identity.IoC/"]
COPY ["src/ConsignadoPrivado.Identity.ORM/ConsignadoPrivado.Identity.ORM.csproj", "src/ConsignadoPrivado.Identity.ORM/"]
RUN dotnet restore "./src/ConsignadoPrivado.Identity.WebApi/ConsignadoPrivado.Identity.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ConsignadoPrivado.Identity.WebApi"
RUN dotnet build "./ConsignadoPrivado.Identity.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConsignadoPrivado.Identity.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsignadoPrivado.Identity.WebApi.dll"]
