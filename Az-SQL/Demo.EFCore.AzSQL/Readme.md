#### Create dotnet console application  
``` Create dotnet console application  
dotnet new console -o Demo.EFCore.SQL
```
#### Install following packages for entityframeworkCore  packages
```Install following packages for entityframeworkCore  packages
dotnet add  package  Microsoft.EntityFrameworkCore.SqlServer 
dotnet add  package  Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.1
```
#### Install following packages to buil startup.cs to build configure services and Service provider Factory
```Install following packages to buil startup.cs to build configure services and Service provider Factory
dotnet add package Microsoft.Extensions.Hosting --version 5.0.0
dotnet add package Microsoft.Extensions.Configuration --version 5.0.0
dotnet add package Microsoft.AspNetCore.Http.Abstractions --version 2.2.0
dotnet add package Microsoft.AspNetCore.Hosting --version 2.2.7
<!-following package is especially for asp.net core webapp  and its not required  for console --->
<!-- dotnet add package Microsoft.AspNetCore.App --version 2.2.8 -->
<!-End --->
dotnet add package Microsoft.Extensions.Logging.Abstractions --version 5.0.0
dotnet add package Microsoft.NET.Sdk.Web
```
#### Install global dotnet-ef tool 
```install global dotnet-ef tool 
dotnet tool install --global dotnet-ef 
```
#### Add connectionstring in appsettings.json
``` Add connectionstring in appsettings.json
{
    "ConnectionStrings": {
      "sqldbDatabase": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=YourDatabase"    <!-- replace with AZ Sql connection string-->
    }

  }
```
#### Initiate User-secrets and set user-secret for connection string 
``` Initiate User-secrets and set user-secret for connection string  
dotnet user-secrets init
echo  "replace with AZ Sql connection string"
dotnet user-secrets set ConnectionStrings:YourDatabaseAlias "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YourDatabase"
```
####  Create domain entities from azure sql using EntityFrameworkCore Scaffold framework 
``` Create domain entities from azure sql using EntityFrameworkCore Scaffold framework 
```
dotnet-ef   dbcontext  scaffold Name=ConnectionStrings:sqldbDatabase    Microsoft.EntityFrameworkCore.SqlServer  -o Models
