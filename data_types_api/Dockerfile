#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
WORKDIR /app/LoadFiles
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["data_types_api/data_types_api.csproj", "data_types_api/"]
RUN dotnet restore "data_types_api/data_types_api.csproj"
COPY . .
WORKDIR "/src/data_types_api"
RUN dotnet build "data_types_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "data_types_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
WORKDIR /app/LoadFiles
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "data_types_api.dll"]