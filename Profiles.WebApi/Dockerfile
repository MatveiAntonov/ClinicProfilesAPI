#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Profiles.WebApi/Profiles.WebApi.csproj", "Profiles.WebApi/"]
COPY ["Profiles.Application/Profiles.Application.csproj", "Profiles.Application/"]
COPY ["Profiles.Domain/Profiles.Domain.csproj", "Profiles.Domain/"]
COPY ["Profiles.Persistence/Profiles.Persistence.csproj", "Profiles.Persistence/"]
RUN dotnet restore "Profiles.WebApi/Profiles.WebApi.csproj"
COPY . .
WORKDIR "/src/Profiles.WebApi"
RUN dotnet build "Profiles.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profiles.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8008
ENTRYPOINT ["dotnet", "Profiles.WebApi.dll"]