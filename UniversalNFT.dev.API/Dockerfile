FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /UniversalNFT.dev.API

COPY ["UniversalNFT.dev.API.csproj", ""]
RUN dotnet restore "UniversalNFT.dev.API.csproj"

COPY . .
RUN dotnet publish "UniversalNFT.dev.API.csproj" -c Release  -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 5103

CMD  ["export", "DOTNET_CLI_TELEMETRY_OPTOUT=1"]

ENTRYPOINT ["dotnet", "UniversalNFT.dev.API.dll"]