# Getting Started

## Instructions
### Installation:
1. Install the latest [.NET Core 7 SDK](https://dotnet.microsoft.com/download). 
2. Run `dotnet new --install Net.WebApi.Template::1.0.0-beta.4` to install the project template

#### Using dotnet CLI:
Run `dotnet new net7webapi` to create a project from the template.

### Using Docker
Make sure that you have installed docker and cofigured Docker in you environment. After that you can run the below command from
project root directory and get started with .NET Web API Solution immediately.

```
docker-compose build
docker-compose up
```

You should be able to browse API Swagger endpoint:

```
https://localhost:[port]/swagger/index
```

### Database Setup
To setup the SQL Server database following the instructions below:
1. Reveiw the connection string in appsettings.Development.json and update the database name.
2. Run `dotnet ef migrations add Initial --context <ProjectName>DbContext` to add migation with EF Core 
3. Run `dotnet ef database update Initial` to create application database.
