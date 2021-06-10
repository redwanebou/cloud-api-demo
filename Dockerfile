FROM node:alpine AS node_base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY --from=node_base . .
WORKDIR /app
COPY ./AutoAPI/*.csproj ./AutoAPI/
RUN dotnet restore
COPY AutoAPI/. ./AutoAPI/
RUN dotnet publish -c Release -o output
# Runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app/AutoAPI
COPY --from=build-env /app/AutoAPI/output .
ENTRYPOINT ["dotnet", "AutoAPI.dll"]