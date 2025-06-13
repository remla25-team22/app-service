
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY app-service.sln ./
COPY app-service ./app-service
COPY model-service.Connector ./model-service.Connector
RUN git clone --depth 1 --branch v1.0.0 https://github.com/remla25-team22/lib-version.git ./lib-version

RUN dotnet restore
RUN dotnet publish ./app-service/app-service.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENV BackendUrl=http://localhost:8081

EXPOSE 8080
ENTRYPOINT ["dotnet", "app-service.dll"]
