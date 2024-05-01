FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./IntegrationContext/ ./IntegrationContext/
COPY ./Library/ ./Library/
COPY ./MassTransitContracts/ ./MassTransitContracts/

WORKDIR /App/IntegrationContext/
RUN ls
RUN dotnet restore
RUN dotnet publish ./IntegrationContext.Presentation/IntegrationContext.Presentation.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/IntegrationContext/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "IntegrationContext.Presentation.dll"]
