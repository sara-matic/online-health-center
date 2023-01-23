#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Impressions/Impressions.GRPC/Impressions.GRPC.csproj", "Services/Impressions/Impressions.GRPC/"]
COPY ["Services/Impressions/Impressions.Common/Impressions.Common.csproj", "Services/Impressions/Impressions.Common/"]
RUN dotnet restore "Services/Impressions/Impressions.GRPC/Impressions.GRPC.csproj"
COPY . .
WORKDIR "/src/Services/Impressions/Impressions.GRPC"
RUN dotnet build "Impressions.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Impressions.GRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Impressions.GRPC.dll"]