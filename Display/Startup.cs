using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Display
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

        // This method gets called by the runtime. Use this method to add services to the DI container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure logging
            services.AddLogging();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //Repoint to HTTPS
            app.UseHttpsRedirection();

            // Retrieve a static file (favicon.ico, Img, PDF, etc.)
            // Configure StaticFile middleware
            app.UseStaticFiles();

            // Configure MVC page middleware with non default routing
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            //Catch all for any pages that didn't use another middleware
            app.Run((context) =>
            {
                logger.LogWarning(context.Request.Path + " - Request not handled by other middleware: " + context.Request.Path);
                //return new Task<ActionResult>().Status = NotFoundResult();
                throw new Exception(context.Request.Path + " - Request not handled by other middleware");
            });
        }
    }
}
