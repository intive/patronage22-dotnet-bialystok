FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /src #/app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Patronage.Api/Patronage.Api.csproj", "src/Patronage.Api/"]
COPY ["src/Patronage.Common/Patronage.Common.csproj", "src/Patronage.Common/"]
COPY ["src/Patronage.DataAccess/Patronage.DataAccess.csproj", "src/Patronage.DataAccess/"]
COPY ["src/Patronage.Models/Patronage.Models.csproj", "src/Patronage.Models/"]
COPY ["src/Patronage.Migrations/Patronage.Migrations.csproj", "src/Patronage.Migrations/"]

RUN dotnet restore "src/Patronage.Api/Patronage.Api.csproj"
COPY . .
WORKDIR "/src/src/Patronage.Api"
RUN dotnet build "Patronage.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Patronage.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /src #/app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Patronage.Api.dll"]