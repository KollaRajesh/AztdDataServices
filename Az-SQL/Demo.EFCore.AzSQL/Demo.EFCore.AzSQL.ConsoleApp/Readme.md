### Create dotnet console application  
``` Create dotnet console application  
dotnet new console -o Demo.EFCore.AzSQL.ConsoleApp

dotnet new sln -n  Demo.EFCore.AzSQL

dotnet sln add  .\Demo.EFCore.AzSQL.ConsoleApp\Demo.EFCore.AzSQL.ConsoleApp.csproj
```


#### Install following packages for entityframeworkCore  packages
```Install following packages for entityframeworkCore  packages

dotnet add  package  Microsoft.EntityFrameworkCore.SqlServer 
dotnet add  package  Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.1
```

#### Install global dotnet-ef tool 
```install global dotnet-ef tool 
dotnet tool install --global dotnet-ef 
```

#### Initiate User-secrets and set user-secret for connection string 
``` Initiate User-secrets and set user-secret for connection string  
dotnet user-secrets init

or 
{
  "UserSecretsId":"Guid for SecreId"
}

echo  "replace with AZ Sql connection string"
dotnet user-secrets set ConnectionStrings:YourDatabaseAlias "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YourDatabase"

or 
 Add  below config in secrets.config
{
  "UserSecretsId":"Guid for SecreId",
    "ConnectionStrings": {
      "sqldbDatabase": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=YourDatabase"
    }
}

```


#### Install following packages to build configure services and Service provider Factory
``` Install following packages to build configure services and Service provider Factory

<!-- Install following packages to build configure services and Service provider Factory -->

  dotnet add package Microsoft.Extensions.Hosting --version 5.0.0
  dotnet add package Microsoft.Extensions.Configuration --version 5.0.0
  dotnet add package Microsoft.Extensions.Logging.Abstractions --version 5.0.0
  

<!-following package is especially for asp.net core webapp  so these are not required to install  for console app--->
      <!-- Begin -->
        dotnet add package Microsoft.AspNetCore.Http.Abstractions --version 2.2.0
        dotnet add package Microsoft.AspNetCore.Hosting --version 2.2.7
        dotnet add package Microsoft.AspNetCore.App --version 2.2.8   <!--(obsolete) -->
        dotnet add package Microsoft.NET.Sdk.Web

      <!-End --->
<!-- Following function is used to create hostbuilder and add configuration providers and configure services in host builder -->
 public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
             .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??"Development")
             .ConfigureHostConfiguration(configHost => 
             {
               //Configuring Host configuration
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile(_hostsettings, optional: true);
                    configHost.AddEnvironmentVariables();
                    configHost.AddCommandLine(args);
             })
             .ConfigureAppConfiguration((hostContext, configApp)=>{ 
               //Configuring App configuration
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile(_appsettings, optional: true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                                            optional: true);
                    configApp.AddEnvironmentVariables(prefix: _prefix);
                   if (hostContext.HostingEnvironment.IsDevelopment())
                   {
                     
                     configApp.AddUserSecrets<Program>();
                    //configApp.AddUserSecrets("SecretId");

                    }
                    configApp.AddCommandLine(args);
                    

                }
             )
             .ConfigureServices((HostContext,services)=> 
             {
               //configuring services 
                services.AddLogging();
              

             });



```

#### Add DbContext to Service collection in ConfigureServices method. 
 ``` Adding  DbContext to Service collection in ConfigureServices method. 
  
  var database=HostContext.Configuration.GetConnectionString("sqldbDatabase");
                services.AddDbContext<sqldbaztdContext>(options =>
                options.UseSqlServer(database));
```



####  Create domain entities from azure sql using EntityFrameworkCore Scaffold framework 
``` Create domain entities from azure sql using EntityFrameworkCore Scaffold framework 

dotnet-ef   dbcontext  scaffold Name=ConnectionStrings:sqldbDatabase  Microsoft.EntityFrameworkCore.SqlServer -c ProductsContext -o Models

```

### Adding Logging to DB Context
```  Adding Logging to DB Context
<!--Adding console package  -->
dotnet add package Microsoft.Extensions.Logging.Console

    //static LoggerFactory object
    public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((_, __) => true, true)

                .EnableSensitiveDataLogging()  
```