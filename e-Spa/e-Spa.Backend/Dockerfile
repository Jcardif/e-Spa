#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["e-Spa.Backend/e-Spa.Backend.csproj", "e-Spa.Backend/"]
RUN dotnet restore "e-Spa.Backend/e-Spa.Backend.csproj"
COPY . .
WORKDIR "/src/e-Spa.Backend"
RUN dotnet build "e-Spa.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "e-Spa.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "e-Spa.Backend.dll"]