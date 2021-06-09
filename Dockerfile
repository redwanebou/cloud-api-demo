FROM node:alpine AS node_base
FROM mcr.microsoft.com/dotnet/core/sdk3.1 AS build-env
COPY --from=node_base . .