using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using InventoryService.Infrastructure.Data;
using InventoryService.Infrastructure.Middleware.Filters;
using InventoryService.Infrastructure.Middleware.Logging;
using InventoryService.Infrastructure.Middleware.Logging.Web.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using InventoryService.Data.Models;
using InventoryService.Repository;
using InventoryService.Repository.DIExtensions;

namespace InventoryService
{
    /// <summary>
    /// Startup section for configuring and using DI Services
    /// </summary>
    /// <remarks>This class will invoke middleware registered services.</remarks>
    public class Startup
    {
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// reference to Configuration object file
        /// </summary>
        /// <remarks>This interface object will hold values from appsettings.json.</remarks>
        public IConfigurationRoot Configuration { get; }

        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// Build Configuration from appsettings.json file
        /// </summary>
        /// <remarks>This constructor will retrieve values from appsettings.json.</remarks>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// Configure .NET DI Services
        /// </summary>
        /// <remarks>This API will register all the DI services.</remarks>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region LightInject Configuration

            var containerOptions = new ContainerOptions { EnablePropertyInjection = false };
            var container = new ServiceContainer(containerOptions);

            #endregion

            var sqlConnection = Configuration.GetConnectionString("InventoryDb");
            services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(sqlConnection));

            var sqlLogConnection = Configuration.GetConnectionString("LogManagerDb");
            services.AddDbContext<LogManagerDbContext>(options => options.UseSqlServer(sqlLogConnection));

            //Register your own services within LightInject
            container.Register<IUnitOfWork, UnitOfWork<InventoryDbContext>>();

            services.AddRouting();

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true; // false by default 
                options.Filters.Add(typeof(LogActionFilter));
            }).AddControllersAsServices();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<LogFilter>();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme()
                {
                    Description = "Authorization format : Bearer {token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Inventory Web API",
                    Description = "An ASP.NET Core 2.0 Template",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "KantiKiran Bhupathi", Email = "kantikiran2689@gmail.com", Url = "https://www.linkedin.com/in/kanti-kiran-bhupathi-a9a5028/" }
                });
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.DescribeAllEnumsAsStrings();
            });

            return container.CreateServiceProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Will use to configure all .NET DI Services
        /// </summary>
        /// <remarks>This API will add all the DI services.</remarks>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }

            app.UseExceptionHandler("/Home/Error");

            app.UseStatusCodePages();

            app.UseStaticFiles();

            //configuring logging incoming request and responses..
            app.UseMiddleware<LoggerMiddleware>();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Unable to route your request to controller.");
            });
        }

        //swagger xml documentation path..
        /// <summary>
        /// Will get the xml comments file path created at bin debug/release folder
        /// </summary>
        /// <remarks>This API will use xml comments for Open API Specification.</remarks>
        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return System.IO.Path.Combine(app.ApplicationBasePath, "InventoryService.xml");
        }
    }
}
