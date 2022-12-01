using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace TheNomad.EFCore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            //Log.Logger = new LoggerConfiguration()
            //                .ReadFrom.Configuration(Configuration)
            //                .Enrich.FromLogContext()
            //                .CreateLogger();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //.UseSerilog()
                .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    configurationBuilder.AddEnvironmentVariables();
                });
    }
}
