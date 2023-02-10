<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/marlonajgayle/Net7WebApiTemplate/develop/src/content/.template.config/icon.png" />

# dotnet core web api template
[![Build](https://github.com/marlonajgayle/Net7WebApiTemplate/actions/workflows/dotnet.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/Net7WebApiTemplate/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/marlonajgayle/Net7WebApiTemplate/actions/workflows/codeql-analysis.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/Net7WebApiTemplate/actions/workflows/codeql-analysis.yml)

A modern multi-project .NET template that utilises Domain Driven Design (DDD) and architecture to implement a maintainable enterprise-level API application 
that provides Api versioning, Fluent email, Fluent validation, JWT authentication, Identity role-based authorization, InMemory caching, IP rate limiting, 
Mediator, Sirilog and Swagger.


## Table of Contents
* [Prerequisites](#Prerequisites)
* [Instructions](#Instructions)
* [Contributions](#Contributions)
* [Credits](#Credits)


## Prerequisites
You will need the following tools:
* [Visual Studio Code or Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (version 17.0.0 or later)
* [.NET Core SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)


### Database Setup
To setup the SQL Server database following the instrcutions below:
1. Reveiw the connection string in appsettings.Local.json and update the database name.
2. Run `dotnet ef migrations add Initial --context <ProjectName>DbContext` to add migation with EF Core 
3. Run `dotnet ef database update Initial` to create application database.

## Contributions


## Credits
Icon made by [DinosoftLabs](a href="https://www.flaticon.com/free-icons/api) from [www.flaticon.com](https://www.flaticon.com/)

## Versions
The [main](https://github.com/marlonajgayle/Net7WebApiTemplate/main) branch is running .NET 7.0

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/marlonajgayle/Net7WebApiTemplate/main/LICENSE.md) [main](https://github.com/marlonajgayle/Net6WebApiTemplate/main) branch is running .NET 7.0
file for details.