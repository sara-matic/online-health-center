#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Impressions/Impressions.API/Impressions.API.csproj", "Services/Impressions/Impressions.API/"]
RUN dotnet restore "Services/Impressions/Impressions.API/Impressions.API.csproj"
COPY . .
WORKDIR "/src/Services/Impressions/Impressions.API"
RUN dotnet build "Impressions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Impressions.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Impressions.API.dll"]