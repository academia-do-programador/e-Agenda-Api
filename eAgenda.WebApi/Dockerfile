#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Etapas de build

# Preparando a minha imagem

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["eAgenda.WebApi/eAgenda.WebApi.csproj", "eAgenda.WebApi/"]
COPY ["eAgenda.Aplicacao/eAgenda.Aplicacao.csproj", "eAgenda.Aplicacao/"]
COPY ["eAgenda.Dominio/eAgenda.Dominio.csproj", "eAgenda.Dominio/"]
COPY ["eAgenda.Infra.Orm/eAgenda.Infra.Orm.csproj", "eAgenda.Infra.Orm/"]
RUN dotnet restore "eAgenda.WebApi/eAgenda.WebApi.csproj"
COPY . .
WORKDIR "/src/eAgenda.WebApi"
RUN dotnet build "eAgenda.WebApi.csproj" -c Release -o /app/build

# Etapa de publicação

FROM build AS publish
RUN dotnet publish "eAgenda.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eAgenda.WebApi.dll"]