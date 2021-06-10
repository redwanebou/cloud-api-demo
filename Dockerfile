FROM node:alpine AS node_base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY --from=node_base . .
WORKDIR /app
COPY *.csproj ./DevAutoAPI/
RUN dotnet restore
COPY DevAutoAPI/. ./DevAutoAPI/
RUN dotnet publish -c Release -o output
# Runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app/DevAutoAPI
COPY --from=build-env /app/DevAutoAPI/output .
ENTRYPOINT ["dotnet", "AutoAPI.dll"]