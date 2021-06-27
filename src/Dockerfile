FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base

LABEL author="Arthur Safiullin"

WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ./websocketschat.Web/websocketschat.Web.csproj/ ./websocketschat.Web/
RUN dotnet restore "websocketschat.Web/websocketschat.Web.csproj"
COPY . .
WORKDIR "/src/websocketschat.Web"
RUN dotnet build "websocketschat.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "websocketschat.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "websocketschat.Web.dll"]