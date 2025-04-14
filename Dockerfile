# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia todo el proyecto
COPY . .

WORKDIR "/src/CinemaAPI"
RUN dotnet restore "CinemaAPI.csproj"
RUN dotnet build "CinemaAPI.csproj" -c Release -o /app/build

# Etapa publish
FROM build AS publish
RUN dotnet publish "CinemaAPI.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaAPI.dll"]
