
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
 using Demo.EFCore.AzSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Demo.EFCore.AzSQL
{
public class Startup
{
     public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public Startup(IHostingEnvironment environment)
    {
         var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                        //.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                    .AddJsonFile($"appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();
        if (environment.IsDevelopment())
        {
            builder.AddUserSecrets<Program>();
        }

       Configuration= builder.Build();
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {

        var database=Configuration.GetConnectionString("sqldbDatabase");
        services.AddDbContext<sqldbaztdContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("sqldbDatabase")));
    }

//  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//     {
//     }
}
}