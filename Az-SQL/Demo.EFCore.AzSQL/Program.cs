using System;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Demo.EFCore.AzSQL.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.IO;

namespace Demo.EFCore.AzSQL
{
   public  class Program
    {
        public static void Main(string[] args)
        {
                
        // var configuration =  ConfigureBuilder().Build();
        // IServiceCollection services = new ServiceCollection();
        // Startup startup = new Startup(configuration);
        // startup.ConfigureServices(services);
        // IServiceProvider serviceProvider = services.BuildServiceProvider();
        
        using (var context= ServiceProviderFactory.ServiceProvider.GetService<sqldbaztdContext>()){
            foreach (var product in context.Products)
            {
                Console.WriteLine("Product Name : {0} and price :{1}" , product.Name, product.ListPrice);
            }

        }

        }



        //  public static IHostBuilder CreateHostBuilder(string[] args) =>
        //      Host.CreateDefaultBuilder(args)
        //     .ConfigureWebHostDefaults(webBuilder =>
        //     {
        //         webBuilder.UseStartup<Startup>();
        //     });
    }
}
