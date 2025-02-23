FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY *.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o Release

FROM mcr.microsoft.com/dotnet/aspnet:8.0 

WORKDIR /app

COPY --from=build /app/Release ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "bakery.api.dll"]