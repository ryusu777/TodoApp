FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./ApiGateway/ApiGateway.csproj ./

FROM build-env AS dependencies
RUN dotnet restore

FROM dependencies AS release
COPY ./ApiGateway/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=release /App/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
