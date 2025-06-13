FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

ARG NUGET_AUTH_TOKEN

COPY app-service.sln ./
COPY app-service ./app-service
COPY model-service.Connector ./model-service.Connector

RUN dotnet nuget add source "https://nuget.pkg.github.com/remla25-team22/index.json" \
    --name github \
    --username x-access-token \
    --password $NUGET_AUTH_TOKEN \
    --store-password-in-clear-text

RUN dotnet restore
RUN dotnet publish ./app-service/app-service.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENV BackendUrl=http://localhost:8081

EXPOSE 8080
ENTRYPOINT ["dotnet", "app-service.dll"]
