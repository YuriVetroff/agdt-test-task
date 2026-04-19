FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet publish AgdtTestTask.Medical.DataAccess.Migrations/AgdtTestTask.Medical.DataAccess.Migrations.csproj \
    -c Release \
    -o /publish

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

COPY --from=build /publish .

ENTRYPOINT ["dotnet", "AgdtTestTask.Medical.DataAccess.Migrations.dll", "Migrate"]
