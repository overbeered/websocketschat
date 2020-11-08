FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY . .

RUN dotnet restore
WORKDIR /src/websocketschat.Web
RUN dotnet publish "websocketschat.Web.csproj" -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build-env /app .
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "websocketschat.Web.dll"]
