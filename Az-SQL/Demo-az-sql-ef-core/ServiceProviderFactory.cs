using System;
using System.IO;
using demo_1_az_sql_ef_core;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceProviderFactory
{
    public static IServiceProvider ServiceProvider { get; }

    static ServiceProviderFactory()
    {
        HostingEnvironment env = new HostingEnvironment();
        env.ContentRootPath = Directory.GetCurrentDirectory();
           var EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
           if (string.IsNullOrWhiteSpace(EnvironmentName))
           {
                env.EnvironmentName = "Development";
           }else
            {
            env.EnvironmentName=EnvironmentName;

            }
        Startup startup = new Startup(env);
        ServiceCollection sc = new ServiceCollection();
        startup.ConfigureServices(sc);
        ServiceProvider = sc.BuildServiceProvider();
    }
}