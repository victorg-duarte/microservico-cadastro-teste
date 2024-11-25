FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app

#HTTP
EXPOSE 5188

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Cadastro.API/Cadastro.API.csproj", "src/Cadastro.API/"]
RUN dotnet restore "./src/Cadastro.API/Cadastro.API.csproj"
COPY . .
WORKDIR "/src/src/Cadastro.API"
RUN dotnet build "./Cadastro.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cadastro.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:5188

ENTRYPOINT ["dotnet", "Cadastro.API.dll"]