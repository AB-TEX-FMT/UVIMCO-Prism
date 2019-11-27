using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NLog.Extensions.Logging;
using DataRepository.Factories;
using DataRepository;
using DataRepository.NPocoRepository;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using DataRepository.MemoryRepository;

namespace DataService
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            // Configure logging
            services.AddLogging();

            // Load configuration options from appSettings
            IConfigurationSection sec = Configuration.GetSection("ServiceOptions");
            services.Configure<ServiceOptions>(sec);
            services.Configure<ServiceOptions>(Configuration.GetSection("ServiceOptions"));

            // Loads the configuration to an object for use below
            // TODO: Figure out a better way to do this. It works but it's a bit kludgy.
            ServiceProvider sp = services.BuildServiceProvider();
            IOptions<ServiceOptions> iop = sp.GetService<IOptions<ServiceOptions>>();
            ServiceOptions so = iop.Value;

            // Configure the Data base Factory
            services.AddScoped<IDBFactory, DbFactory>(x => new DbFactory(so.AuthenticationDBConnectionString, so.PrismDBConnectionString));

            if (so.UseInMemoryRepository)
            {
                // Use the In Memory option
                services.AddScoped<IPrismRepository, MemoryPrismRepository>();
            }
            else
            {
                // Use the actual DB
                services.AddScoped<IPrismRepository, NPocoPrismRepository>();
            }

            // Configure the User Repository
            //services.AddScoped<IAuthenticationRepository, NPocoAuthenticationRepository>();
        }
    }
}
