FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /src

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Patronage.Api/Patronage.Api.csproj", "src/Patronage.Api/"]
COPY ["src/Patronage.Common/Patronage.Common.csproj", "src/Patronage.Common/"]
COPY ["src/Patronage.Contracts/Patronage.Contracts.csproj", "src/Patronage.Contracts/"]
COPY ["src/Patronage.DataAccess/Patronage.DataAccess.csproj", "src/Patronage.DataAccess/"]
COPY ["src/Patronage.Models/Patronage.Models.csproj", "src/Patronage.Models/"]
COPY ["src/Patronage.MigrationsPostgre/Patronage.MigrationsPostgre.csproj", "src/Patronage.MigrationsPostgre/"]


RUN dotnet restore "src/Patronage.Api/Patronage.Api.csproj"
COPY . .

WORKDIR "/src/src/Patronage.Api"
RUN dotnet build "Patronage.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Patronage.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /src
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Patronage.Api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Patronage.Api.dll
