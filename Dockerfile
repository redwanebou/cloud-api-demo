FROM node:alpine AS node_base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY --from=node_base . .
EXPOSE 8081
ENV ASPNETCORE_URLS=http://*:8081