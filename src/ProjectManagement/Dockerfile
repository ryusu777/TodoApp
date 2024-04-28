FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./ProjectManagement/ ./ProjectManagement/
COPY ./Library/ ./Library/

WORKDIR /App/ProjectManagement/
RUN ls
RUN dotnet restore
RUN dotnet publish ./ProjectManagement.Presentation/ProjectManagement.Presentation.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/ProjectManagement/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "ProjectManagement.Presentation.dll"]
