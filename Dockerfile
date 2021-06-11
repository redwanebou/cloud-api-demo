FROM node:alpine AS node_base #import for npm install
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env #import the microsoft image
COPY --from=node_base . .
WORKDIR /app
COPY ./AutoAPI/*.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o output
#next step
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/output .
EXPOSE 3000
ENTRYPOINT ["dotnet", "AutoAPI.dll"]