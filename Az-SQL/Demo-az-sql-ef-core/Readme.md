

dotnet add  package  Microsoft.EntityFrameworkCore.SqlServer 
dotnet add  package  Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.Extensions.Hosting --version 5.0.0
dotnet add package Microsoft.Extensions.Configuration --version 5.0.0
dotnet add package Microsoft.AspNetCore.Http.Abstractions --version 2.2.0
dotnet add package Microsoft.AspNetCore.Hosting --version 2.2.7
<!-- dotnet add package Microsoft.AspNetCore.App --version 2.2.8 -->
dotnet add package Microsoft.Extensions.Logging.Abstractions --version 5.0.0
dotnet add package Microsoft.NET.Sdk.Web
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.1



dotnet tool install --global dotnet-ef 

dotnet user-secrets init
dotnet user-secrets set ConnectionStrings.YourDatabaseAlias "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YourDatabase"


dotnet-ef   dbcontext  scaffold Name=ConnectionStrings.sqldbDatabase    Microsoft.EntityFrameworkCore.SqlServer  -o Models
