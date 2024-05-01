FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./AuthContext/ ./AuthContext/
COPY ./Library/ ./Library/
COPY ./MassTransitContracts/ ./MassTransitContracts/

WORKDIR /App/AuthContext/
RUN ls
RUN dotnet restore
RUN dotnet publish ./AuthContext.Presentation/AuthContext.Presentation.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/AuthContext/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "AuthContext.Presentation.dll"]
