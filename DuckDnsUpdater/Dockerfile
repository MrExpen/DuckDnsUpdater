FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DuckDnsUpdater/DuckDnsUpdater.csproj", "DuckDnsUpdater/"]
RUN dotnet restore "DuckDnsUpdater/DuckDnsUpdater.csproj"
COPY . .
WORKDIR "/src/DuckDnsUpdater"
RUN dotnet build "DuckDnsUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DuckDnsUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DuckDnsUpdater.dll"]
