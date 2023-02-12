<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/marlonajgayle/Net7WebApiTemplate/develop/src/content/.template.config/icon.png" />

# .NET Web API Solution Template
[![Build](https://github.com/marlonajgayle/Net7WebApiTemplate/actions/workflows/dotnet.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/Net7WebApiTemplate/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/marlonajgayle/Net7WebApiTemplate/actions/workflows/codeql-analysis.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/Net7WebApiTemplate/actions/workflows/codeql-analysis.yml)
[![Boilerplate.Templates NuGet Package](https://img.shields.io/nuget/v/Net.WebApi.Template.svg)](https://www.nuget.org/packages/Net.WebApi.Template)
[![Boilerplate.Templates NuGet Package Downloads](https://img.shields.io/nuget/dt/Net.WebApi.Template)](https://www.nuget.org/packages/Net.WebApi.Template)

A modern multi-project.NET project template that implements a maintainable enterprise-level API application with
Api versioning, Fluent email, Fluent validation, JWT authentication, Identity role-based authorization, InMemory caching, 
IP rate limiting, CQRS with Mediator, Sirilog, and Swagger using Domain Driven Design (DDD) and architecture.


## Table of Contents
* [Prerequisites](#Prerequisites)
* [Instructions](#Instructions)
* [Contributions](#Contributions)
* [Credits](#Credits)


## Prerequisites
You will need the following tools:
* [Visual Studio Code or Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (version 17.0.0 or later)
* [.NET Core SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)


## Instructions
### Installation:
1. Install the latest [.NET Core 7 SDK](https://dotnet.microsoft.com/download). 
2. Run `dotnet new --install Net.WebApi.Template::1.0.0-beta.3` to install the project template

#### Using Visual Studio:
Select WebAPI from the project type drop down.
Select the .NET Web API Solution template you want to install and follow the instructions.

#### Using dotnet CLI:
Run `dotnet new net7webapi` along with any other custom options to create a project from the template.


### Database Setup
To setup the SQL Server database following the instructions below:
1. Reveiw the connection string in appsettings.Development.json and update the database name.
2. Run `dotnet ef migrations add Initial --context <ProjectName>DbContext` to add migation with EF Core 
3. Run `dotnet ef database update Initial` to create application database.


## Third Party Libraries
* Dapper
* Fluent Email
* Fluent Validation
* HashidsCore.NET
* Mediator.SourceGenerator
* AspNetCoreRateLimit
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