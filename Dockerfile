# To build this dockerfile, run the following command from the solution directory:
# docker build --tag net7webapi

# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
ARG Configuration=Release
ENV DOTNET_CLI_TELEMETRY_OUTPUT=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
WORKDIR /app

# Copy the csproj file and restore dependencies
COPY "NetWebApiTemaplate.sln" "."
COPY "src/NetWebApiTemaplate.Api/*.csproj" "src/NetWebApiTemaplate.Api/"
COPY "src/NetWebApiTemaplate.Domain/*.csproj" "src/NetWebApiTemaplate.Domain/"
COPY "src/NetWebApiTemaplate.Application/*.csproj" "src/NetWebApiTemaplate.Application/"
COPY "src/NetWebApiTemaplate.Infrastructure/*.csproj" "src/NetWebApiTemaplate.Infrastructure/"
COPY "src/NetWebApiTemaplate.Persistence/*.csproj" "src/NetWebApiTemaplate.Persistence/"
RUN dotnet restore

# Copy the project files and build release
COPY "src/NetWebApiTemaplate.Api/." "src/NetWebApiTemaplate.Api/"
COPY "src/NetWebApiTemaplate.Domain/." "src/NetWebApiTemaplate.Domain/"
COPY "src/NetWebApiTemaplate.Application/." "src/NetWebApiTemaplate.Application/"
COPY "src/NetWebApiTemaplate.Infrastructure/." "src/NetWebApiTemaplate.Infrastructure/"
COPY "src/NetWebApiTemaplate.Persistence/." "src/NetWebApiTemaplate.Persistence/"
RUN dotnet build "src/NetWebApiTemaplate.Api/src/NetWebApiTemaplate.Api.csproj" --configuration $Configration
RUN dotnet publish "src/NetWebApiTemaplate.Api/src/NetWebApiTemaplate.Api.csproj" --configuration $Configration --no-build out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
EXPOSE 80 443
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "NetWebApiTemplate.Api.dll"]