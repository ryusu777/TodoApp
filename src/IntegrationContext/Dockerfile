FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./IntegrationContext/*/*.csproj .
RUN for file in $(ls *.csproj); do mkdir -p IntegrationContext/${file%.*}/ && mv $file ./IntegrationContext/${file%.*}/; done
COPY ./Library/Library.csproj ./Library/
COPY ./MassTransitContracts/MassTransitContracts.csproj ./MassTransitContracts/
COPY ./IntegrationContext/IntegrationContext.sln ./IntegrationContext/

FROM build-env AS dependencies
WORKDIR /App/IntegrationContext/
RUN dotnet restore

FROM dependencies AS release
WORKDIR /App
COPY ./IntegrationContext/ ./IntegrationContext/
COPY ./Library/ ./Library/
COPY ./MassTransitContracts/ ./MassTransitContracts/
WORKDIR /App/IntegrationContext
RUN dotnet publish ./IntegrationContext.Presentation/IntegrationContext.Presentation.csproj -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=release /App/IntegrationContext/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "IntegrationContext.Presentation.dll"]
