# Usa la imagen oficial de .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usa la imagen de construcción de .NET
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["CinemaAPI.csproj", "./"]

RUN dotnet restore "CinemaAPI.csproj"

WORKDIR "/src"

RUN dotnet build "CinemaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaAPI.dll"]
