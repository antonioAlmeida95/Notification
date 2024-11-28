FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV ASPNETCORE_URLS=http://+:8080/
EXPOSE 8080
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet build ./Adapters/Driving/Service/Notification.Adapter.Driving.Service/Notification.Adapter.Driving.Service.csproj -c Release -o /app/build --runtime=linux-x64
FROM build AS publish

RUN dotnet publish ./Adapters/Driving/Service/Notification.Adapter.Driving.Service/Notification.Adapter.Driving.Service.csproj -c Release -o /app/publish --runtime=linux-x64
FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Adapter.Driving.Service.dll"]