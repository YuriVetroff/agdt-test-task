FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet publish AgdtTestTask.Medical.WebApi/AgdtTestTask.Medical.WebApi.csproj \
    -c Release \
    -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

EXPOSE 8080

COPY --from=build /publish .

ENTRYPOINT ["dotnet", "AgdtTestTask.Medical.WebApi.dll"]
