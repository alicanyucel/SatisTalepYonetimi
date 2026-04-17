FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SatisTalepYonetimi/SatisTalepYonetimi.WebAPI/SatisTalepYonetimi.WebAPI.csproj", "SatisTalepYonetimi/SatisTalepYonetimi.WebAPI/"]
COPY ["SatisTalepYonetimi/SatisTalepYonetimi.Application/SatisTalepYonetimi.Application.csproj", "SatisTalepYonetimi/SatisTalepYonetimi.Application/"]
COPY ["SatisTalepYonetimi/SatisTalepYonetimi.Domain/SatisTalepYonetimi.Domain.csproj", "SatisTalepYonetimi/SatisTalepYonetimi.Domain/"]
COPY ["SatisTalepYonetimi/SatisTalepYonetimi.Infrastructure/SatisTalepYonetimi.Infrastructure.csproj", "SatisTalepYonetimi/SatisTalepYonetimi.Infrastructure/"]
RUN dotnet restore "SatisTalepYonetimi/SatisTalepYonetimi.WebAPI/SatisTalepYonetimi.WebAPI.csproj"
COPY . .
WORKDIR "/src/SatisTalepYonetimi/SatisTalepYonetimi.WebAPI"
RUN dotnet build "SatisTalepYonetimi.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SatisTalepYonetimi.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SatisTalepYonetimi.WebAPI.dll"]