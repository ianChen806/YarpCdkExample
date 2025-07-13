FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y curl
USER $APP_UID
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["YarpProxy/YarpProxy.csproj", "YarpProxy/"]
RUN dotnet restore "YarpProxy/YarpProxy.csproj"
COPY . .
WORKDIR "/src/YarpProxy"
RUN dotnet build "./YarpProxy.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YarpProxy.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YarpProxy.dll"]
