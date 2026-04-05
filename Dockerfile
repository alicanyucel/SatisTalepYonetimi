FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

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

RUN apt-get update && apt-get install -y curl gnupg2 && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "SatisTalepYonetimi.WebAPI.dll"]