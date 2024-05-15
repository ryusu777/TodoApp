FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./ProjectManagement/*/*.csproj .
RUN for file in $(ls *.csproj); do mkdir -p ProjectManagement/${file%.*}/ && mv $file ./ProjectManagement/${file%.*}/; done
COPY ./Library/Library.csproj ./Library/
COPY ./MassTransitContracts/MassTransitContracts.csproj ./MassTransitContracts/
COPY ./ProjectManagement/ProjectManagement.sln ./ProjectManagement/

FROM build-env AS dependencies
WORKDIR /App/ProjectManagement/
RUN dotnet restore

FROM dependencies AS release
WORKDIR /App
COPY ./ProjectManagement/ ./ProjectManagement/
COPY ./Library/ ./Library/
COPY ./MassTransitContracts/ ./MassTransitContracts/
WORKDIR /App/ProjectManagement
RUN dotnet publish ./ProjectManagement.Presentation/ProjectManagement.Presentation.csproj -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=release /App/ProjectManagement/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "ProjectManagement.Presentation.dll"]
