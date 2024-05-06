FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./AuthContext/*/*.csproj .
RUN for file in $(ls *.csproj); do mkdir -p AuthContext/${file%.*}/ && mv $file ./AuthContext/${file%.*}/; done
COPY ./Library/Library.csproj ./Library/
COPY ./MassTransitContracts/MassTransitContracts.csproj ./MassTransitContracts/
COPY ./AuthContext/AuthContext.sln ./AuthContext/

FROM build-env AS dependencies
WORKDIR /App/AuthContext/
RUN dotnet restore

FROM dependencies AS release
WORKDIR /App
COPY ./AuthContext/ ./AuthContext/
COPY ./Library/ ./Library/
COPY ./MassTransitContracts/ ./MassTransitContracts/
WORKDIR /App/AuthContext
RUN dotnet publish ./AuthContext.Presentation/AuthContext.Presentation.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=release /App/AuthContext/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "AuthContext.Presentation.dll"]
