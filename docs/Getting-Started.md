# Getting Started

## Prerequisites
You will need the following tools:
* [Visual Studio Code](https://code.visualstudio.com/download) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (version 17.5.0 or later)
* [.NET Core SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Instructions
### Installation:
1. Install the latest [.NET Core 7 SDK](https://dotnet.microsoft.com/download). 
2. Run `dotnet new --install Net.WebApi.Template::1.0.0-rc.1` to install the project template

```
C:\> dotnet new install Net.WebApi.Template::1.0.0-rc.1
The following template packages will be installed:
   Net.WebApi.Template::1.0.0-rc.1

Success: Net.WebApi.Template::1.0.0-rc.1 installed the following templates:
Template Name                   Short Name  Language  Tags
------------------------------  ----------  --------  ------------------------
.NET Web API Solution Template  net7webapi  [C#]      WebAPI/Cloud/Service/Web
```

### Using Visual Studio Code w/ Docker
Ensure you have installed and configured docker in your environment as this will be required to run the containers.
After installing .NET Web API Solution Template(Net.WebApi.Template::1.0.0-rc.1) 
Launch Visual Studio Code and create your project folder.
Open the VS Code terminal window and execute the following command to create your application:

```
dotnet new net7webapi --name MyProject
```

#### Create initial database migration
1. Nagigate into the project folder `cd MyProject`.
2. Install `dotnet ef` as a global tool with the following command:

```
dotnet tool install --global dotnet-ef
```
```
You can invoke the tool using the following command: dotnet-ef
Tool 'dotnet-ef' (version '7.0.4') was successfully installed.
```

3. Next create initial database migration with the following command:

```
dotnet ef migrations add InitialCreate --context <ProjectNameDbContext>\
--startup-project src/ProjectName.Api/ProjectName.Api.csproj\
--project src/ProjectName.Persistence/ProjectName.Persistence.csproj
```

```
Build started...
Build succeeded.
Development
Server=(localdb)\MSSQLLocalDB;Database=application_db;Trusted_Connection=True;MultipleActiveResultSets=true
Done. To undo this action, use 'ef migrations remove'
PS C:\rojects\MyProject> 
```

By default, the project applies database migrations on startup. If you want disable this behaviour, you can set appsettings.Local.json

```
{
	"UseDatabaseInitializer": true
}
```

#### Launch project with Docker
Web API uses HTTPS and relies on certificates for trust, identiy and encryption. 
To run the template project using Docker over HTTPS during development do the following:
1. Generate certificate using 'dotnet dev-certs' (for localhost only!):

**Note**
Update docker-compose file with dev-cert password used.

On Windows using Linux containers

```
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx"  -p your_password
dotnet dev-certs https --trust
```

**Note**
When using PowerShell, replace `%USERPROFILE%` with `$env:USERPROFILE`.

On macOS or Linux
```
dotnet dev-certs https -ep "${HOME}\.aspnet\https\aspnetapp.pfx"  -p your_password
dotnet dev-certs https --trust
```

To launch the application immediately execute the Docker compose commands
from the root of your project:

```
docker-compose up --build
```

While building the docker images, you should see something like the following image, and the process should take between 
1 and 3 minutes to complete, depending on your systems speed.

```
[+] Building 8.2s (26/26) FINISHED
 => [internal] load build definition from Dockerfile                                                                                                                                0.0s
 => => transferring dockerfile: 32B                                                                                                                                                 0.0s
 => [internal] load .dockerignore                                                                                                                                                   0.0s
 => => transferring context: 2B                                                                                                                                                     0.0s
 => [internal] load metadata for mcr.microsoft.com/dotnet/aspnet:7.0-alpine                                                                                                         0.4s
 => [internal] load metadata for mcr.microsoft.com/dotnet/sdk:7.0                                                                                                                   0.3s
 => [stage-1 1/4] FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine@sha256:be84475ff03cf6e2c11e9f730abd27cae6ae6dcdb9fd8dbebf9b787509ee6b4d                                           0.0s
 => [internal] load build context                                                                                                                                                   0.2s
 => => transferring context: 138.91kB                                                                                                                                               0.1s
 => [build-env  1/16] FROM mcr.microsoft.com/dotnet/sdk:7.0@sha256:f712881bafadf0e56250ece1da28ba2baedd03fb3dd49a67f209f9d0cf928e81                                                 0.0s
 => CACHED [build-env  2/16] WORKDIR /app                                                                                                                                           0.0s
 => CACHED [build-env  3/16] COPY *.sln .                                                                                                                                           0.0s 
 => CACHED [build-env  4/16] COPY src/NetWebApiTemplate.Api/*.csproj ./src/NetWebApiTemplate.Api/                                                                                   0.0s 
 => CACHED [build-env  5/16] COPY src/NetWebApiTemplate.Domain/*.csproj ./src/NetWebApiTemplate.Domain/                                                                             0.0s 
 => CACHED [build-env  6/16] COPY src/NetWebApiTemplate.Application/*.csproj ./src/NetWebApiTemplate.Application/                                                                   0.0s 
 => CACHED [build-env  7/16] COPY src/NetWebApiTemplate.Infrastructure/*.csproj ./src/NetWebApiTemplate.Infrastructure/                                                             0.0s 
 => CACHED [build-env  8/16] COPY src/NetWebApiTemplate.Persistence/*.csproj ./src/NetWebApiTemplate.Persistence/                                                                   0.0s 
 => CACHED [build-env  9/16] RUN dotnet restore                                                                                                                                     0.0s 
 => [build-env 10/16] COPY src/NetWebApiTemplate.Api/. ./src/NetWebApiTemplate.Api/                                                                                                 0.1s 
 => [build-env 11/16] COPY src/NetWebApiTemplate.Domain/. ./src/NetWebApiTemplate.Domain/                                                                                           0.0s
 => [build-env 12/16] COPY src/NetWebApiTemplate.Application/. ./src/NetWebApiTemplate.Application/                                                                                 0.1s 
 => [build-env 13/16] COPY src/NetWebApiTemplate.Infrastructure/. ./src/NetWebApiTemplate.Infrastructure/                                                                           0.1s 
 => [build-env 14/16] COPY src/NetWebApiTemplate.Persistence/. ./src/NetWebApiTemplate.Persistence/                                                                                 0.0s
 => [build-env 15/16] WORKDIR /app/src/NetWebApiTemplate.Api                                                                                                                        0.0s 
 => [build-env 16/16] RUN dotnet publish -c Release -o out                                                                                                                          7.1s 
 => CACHED [stage-1 2/4] RUN apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib                                                                          0.0s
 => CACHED [stage-1 3/4] WORKDIR /app                                                                                                                                               0.0s
 => CACHED [stage-1 4/4] COPY --from=build-env /app/src/NetWebApiTemplate.Api/out ./                                                                                                0.0s 
 => exporting to image                                                                                                                                                              0.0s 
 => => exporting layers                                                                                                                                                             0.0s 
 => => writing image sha256:c03a3d62fdb58e63dbdc63d1e39d4b5d435bdc0b3d70c575f990019316a5ff07                                                                                        0.0s 
 => => naming to docker.io/library/NetWebApiTemplate-webapi 
```

You should be able to browse to the application by using the below URL :

```
Swagger : http://localhost:44372/swagger
Health : http://localhost:44372/health
```

