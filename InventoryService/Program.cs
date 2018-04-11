using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace InventoryService
{
    /// <summary>
    /// Starting point of execution
    /// </summary>
    /// <remarks>This class will build middleware.</remarks>
    public class Program
    {
        /// <summary>
        /// Build Configuration settings from appsettings.json file
        /// </summary>
        /// <remarks>This will retrieve values from appsettings.json.</remarks>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

        /// <summary>
        /// Invoke Build web host provider
        /// </summary>
        /// <remarks>This class will call Build web host function</remarks>
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .ReadFrom.Configuration(Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

            try
            {
                Log.Information("Starting Web Host");
                BuildWebHost(args).Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            /*finally
            {
                Log.CloseAndFlush();
            }*/
        }

        /// <summary>
        /// Method Build web host provider
        /// </summary>
        /// <remarks>This class will Return IWebHost</remarks>
        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .UseSerilog() //Serilog setup...
                .Build();
    }
}
