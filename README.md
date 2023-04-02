<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/marlonajgayle/dotnet-web-api-template/develop/src/content/.template.config/icon.png" />

# dotnet Web API Solution Template
[![Build](https://github.com/marlonajgayle/dotnet-web-api-template/actions/workflows/dotnet.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/dotnet-web-api-template/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/marlonajgayle/dotnet-web-api-template/actions/workflows/codeql-analysis.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/dotnet-web-api-template/actions/workflows/codeql-analysis.yml)
[![Boilerplate.Templates NuGet Package](https://img.shields.io/nuget/v/Net.WebApi.Template.svg)](https://www.nuget.org/packages/Net.WebApi.Template)
[![Boilerplate.Templates NuGet Package Downloads](https://img.shields.io/nuget/dt/Net.WebApi.Template)](https://www.nuget.org/packages/Net.WebApi.Template)

A modern multi-project.NET project template that implements a maintainable enterprise-level API application with
Api versioning, Fluent email, Fluent validation, JWT authentication, Identity role-based authorization, InMemory caching, 
IP rate limiting, CQRS with Mediator, Sirilog, and Swagger using Domain Driven Design (DDD) and architecture princliples.


## Table of Contents
* [Prerequisites](#Prerequisites)
* [Architecture Overview](#Architecture-Overview)
* [Instructions](#Instructions)
* [Contributions](#Contributions)
* [Credits](#Credits)


## Prerequisites
You will need the following tools:
* [Visual Studio Code](https://code.visualstudio.com/download) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (version 17.5.0 or later)
* [.NET Core SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)


## Architecture Overview
This is a multi-project solution that utilizes Domain Driven Design (DDD) and CQRS patterns to create a maintainable
web API application using .NET 7 that allows it to run on Linux or Windows and in Docker environments.

Figure.1 Application structure

## Instructions
### Installation:
1. Install the latest [.NET Core 7 SDK](https://dotnet.microsoft.com/download). 
2. Run `dotnet new --install Net.WebApi.Template::1.0.0-beta.6` to install the project template

```
C:\> dotnet new install Net.WebApi.Template::1.0.0-beta.6
The following template packages will be installed:
   Net.WebApi.Template::1.0.0-beta.6

Success: Net.WebApi.Template::1.0.0-beta.6 installed the following templates:
Template Name                   Short Name  Language  Tags
------------------------------  ----------  --------  ------------------------
.NET Web API Solution Template  net7webapi  [C#]      WebAPI/Cloud/Service/Web
```

### Using Visual Studio Code w/ Docker
Ensure you have installed and configured docker in your environment as this will be required to run the containers.
After installing .NET Web API Solution Template(Net.WebApi.Template::1.0.0-beta.6) 
Launch Visual Studio Code and create your project folder.
Open the VS Code terminal window and execute the following command to create your application:

```
dotnet new net7webapi --name MyProject
```

#### Create initial database migration
1. Install `dotnet ef` as a global tool with the following command:

```
dotnet tool install --global dotnet-ef
```
```
You can invoke the tool using the following command: dotnet-ef
Tool 'dotnet-ef' (version '7.0.4') was successfully installed.
```

2. Navigate into the project folder then create initial migration with the following command:

```
dotnet ef migrations add InitialCreate --context <ProjectNameDbContext> --startup-project src/ProjectName.Api/ProjectName.Api.csproj --project src/ProjectName.Persistence/ProjectName.Persistence.csproj
```

By default, the project applies database migration on startup. If you want disable this behaviour, you can set appsettings.Local.json

```
{
	"UseDatabaseInitializer": true
}
```

#### Launch project with Docker
To launch the application immediately execute the Docker compose commands
from the root of your project:

```
docker-compose build
```

While building the docker images, you should see something like the following image, and the process should take between 
1 and 3 minutes to complete, depending on the system speed.

Deploy to the local Docker host 

```
docker-compose up
```

You should be able to browse to the application by using the below URL :

```
Swagger : http://localhost:5001/swagger
Health : http://localhost:5001/health
```

#### Using Visual Studio:
Ensure [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (version 17.5.0 or later) is installed.
Launch Visual Studio 2022 and Select WebAPI from the project type drop down.
Select the .NET Web API Solution template you want to install and follow the instructions.

### Database Setup
To setup the SQL Server database following the instructions below:
1. Reveiw the connection string in appsettings.Development.json and update the database name.
2. Run `dotnet ef migrations add Initial --context <ProjectName>DbContext` to add migation with EF Core 
3. Run `dotnet ef database update Initial` to create application database.


## Third Party Libraries
* AspNetCoreRateLimit
* Dapper
* Fluent Email
* Fluent Validation
* HashidsCore.NET
* Mediator.SourceGenerator
* Polly
* Serilog


## Contributions


## Credits
Icon made by [DinosoftLabs](href="https://www.flaticon.com/free-icons/api) from [www.flaticon.com](https://www.flaticon.com/)

## Versions
The [main](https://github.com/marlonajgayle/Net7WebApiTemplate/main) branch is running .NET 7.0

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/marlonajgayle/Net7WebApiTemplate/main/LICENSE.md) [main](https://github.com/marlonajgayle/Net6WebApiTemplate/main) branch is running .NET 7.0
file for details.
