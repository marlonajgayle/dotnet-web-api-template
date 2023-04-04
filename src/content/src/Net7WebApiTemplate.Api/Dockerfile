# To build this dockerfile, run the following command from the solution directory:
# docker build --tag net7webapi

# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
ARG Configuration=Release
ENV DOTNET_CLI_TELEMETRY_OUTPUT=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
WORKDIR /app

# Copy the csproj file and restore dependencies
COPY "Net7WebApiTemaplate.sln" "."
COPY "src/Net7WebApiTemaplate.Api/*.csproj" "src/Net7WebApiTemaplate.Api/"
COPY "src/Net7WebApiTemaplate.Domain/*.csproj" "src/Net7WebApiTemaplate.Domain/"
COPY "src/Net7WebApiTemaplate.Application/*.csproj" "src/Net7WebApiTemaplate.Application/"
COPY "src/Net7WebApiTemaplate.Infrastructure/*.csproj" "src/Net7WebApiTemaplate.Infrastructure/"
COPY "src/Net7WebApiTemaplate.Persistence/*.csproj" "src/Net7WebApiTemaplate.Persistence/"
RUN dotnet restore

# Copy the project files and build release
COPY "src/Net7WebApiTemaplate.Api/." "src/Net7WebApiTemaplate.Api/"
COPY "src/Net7WebApiTemaplate.Domain/." "src/Net7WebApiTemaplate.Domain/"
COPY "src/Net7WebApiTemaplate.Application/." "src/Net7WebApiTemaplate.Application/"
COPY "src/Net7WebApiTemaplate.Infrastructure/." "src/Net7WebApiTemaplate.Infrastructure/"
COPY "src/Net7WebApiTemaplate.Persistence/." "src/Net7WebApiTemaplate.Persistence/"
RUN dotnet build "src/Net7WebApiTemaplate.Api/src/Net7WebApiTemaplate.Api.csproj" --configuration $Configration
RUN dotnet publish "src/Net7WebApiTemaplate.Api/src/Net7WebApiTemaplate.Api.csproj" --configuration $Configration --no-build out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
EXPOSE 80 443
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Net7WebApiTemplate.Api.dll"]