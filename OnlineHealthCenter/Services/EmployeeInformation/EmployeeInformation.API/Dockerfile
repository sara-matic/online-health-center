#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/EmployeeInformation/EmployeeInformation.API/EmployeeInformation.API.csproj", "Services/EmployeeInformation/EmployeeInformation.API/"]
RUN dotnet restore "Services/EmployeeInformation/EmployeeInformation.API/EmployeeInformation.API.csproj"
COPY . .
WORKDIR "/src/Services/EmployeeInformation/EmployeeInformation.API"
RUN dotnet build "EmployeeInformation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeInformation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeInformation.API.dll"]