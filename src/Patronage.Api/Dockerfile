#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Patronage.Api/Patronage.Api.csproj", "src/Patronage.Api/"]
COPY . .
RUN dotnet restore "src/Patronage.Api/Patronage.Api.csproj"

WORKDIR "/src/src/Patronage.Api"
RUN dotnet build "Patronage.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Patronage.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Patronage.Api.dll"]
