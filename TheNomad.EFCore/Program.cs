using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace TheNomad.EFCore.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            using var app = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                    webHostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                        _ = config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", false, true))
                    .UseStartup<Startup>())
                .UseSerilog(
                    (hostingContext, loggerConfig) =>
                        loggerConfig
                            .ReadFrom.Configuration(Configuration)
                            .Enrich.FromLogContext(),
                    writeToProviders: false)
                .Build();
            app.Run();
        }
    }
}
