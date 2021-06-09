FROM node:alpine AS node_base
FROM mcr.microsfot.com/dotnet/core/sdk3.1 AS build-env
COPY --from=node_base . .