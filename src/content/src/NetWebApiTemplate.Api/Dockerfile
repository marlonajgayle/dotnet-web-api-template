# To build this dockerfile, run the following command from the startup project directory:
# docker build -tag net7webapi .

# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG Configuration=Release
ENV DOTNET_CLI_TELEMETRY_OUTPUT=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
WORKDIR /app

# Copy the csproj files and restore dependencies for each layer
COPY *.sln .
COPY src/NetWebApiTemplate.Api/*.csproj ./src/NetWebApiTemplate.Api/
COPY src/NetWebApiTemplate.Domain/*.csproj ./src/NetWebApiTemplate.Domain/
COPY src/NetWebApiTemplate.Application/*.csproj ./src/NetWebApiTemplate.Application/
COPY src/NetWebApiTemplate.Infrastructure/*.csproj ./src/NetWebApiTemplate.Infrastructure/
COPY src/NetWebApiTemplate.Persistence/*.csproj ./src/NetWebApiTemplate.Persistence/

RUN dotnet restore

# Copy the project files 
COPY src/NetWebApiTemplate.Api/. ./src/NetWebApiTemplate.Api/
COPY src/NetWebApiTemplate.Domain/. ./src/NetWebApiTemplate.Domain/
COPY src/NetWebApiTemplate.Application/. ./src/NetWebApiTemplate.Application/
COPY src/NetWebApiTemplate.Infrastructure/. ./src/NetWebApiTemplate.Infrastructure/
COPY src/NetWebApiTemplate.Persistence/. ./src/NetWebApiTemplate.Persistence/

# Build and publish app
WORKDIR /app/src/NetWebApiTemplate.Api
RUN dotnet publish -c $Configuration -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
# install cultures
RUN apk add --no-cache icu-libs
# Disbale the invariant mode "set in base image"
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 80 443
COPY --from=build-env /app/src/NetWebApiTemplate.Api/out ./
ENTRYPOINT ["dotnet", "NetWebApiTemplate.Api.dll"]