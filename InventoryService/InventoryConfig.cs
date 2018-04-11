using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService
{
    /// <summary>
    /// InventoryConfig
    /// </summary>
    /// <remarks>This class is used to retrieve connectionString info..</remarks>
    public class InventoryConfig
    {
        /// <summary>
        /// Build Configuration from appsettings.json file
        /// </summary>
        /// <remarks>This constructor will retrieve values from appsettings.json.</remarks>
        /// <param name="env"></param>
        public static IConfigurationRoot InventoryConfiguration(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
