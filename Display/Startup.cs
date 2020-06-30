using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataModel.Shared;
using DataRepository;
using DataRepository.Factories;
using DataRepository.MemoryRepository;
using DataRepository.NPocoRepository;
using DataService;
using DataService.Services;
using Display.Authentication;
using Display.Infrastructure;
using Display.Security;
using Display.Utilities;
using Display.Utilities.AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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

            //// Load configuration options from appSettings
            services.AddHttpClientServices(Configuration);

            Action<ApplicationOptions> appOptions = (opt =>
            {
                opt.DataDirectory = Path.Join(Configuration["DataDirectory"], String.Empty);
                opt.ImageDirectory = Path.Join(opt.DataDirectory, Configuration["ImagesDirectory"]);
            });

            services.Configure(appOptions);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ApplicationOptions>>().Value);

            Action<ServiceOptions> servOptions = (opt =>
            {
                opt.BaseAddress = Configuration["ServicesBase"];
                opt.AuthenticationDBConnectionString = Path.Join(opt.BaseAddress, Configuration["AuthenticationDBConnectionString"]);
                opt.PrismDBConnectionString = Path.Join(opt.BaseAddress, Configuration["PrismDBConnectionString"]);
            });

            services.Configure(servOptions);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ServiceOptions>>().Value);

            // Configure Cookie Service
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configure AutoMapper service - Display.AutoMapper.Mapping
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //See if we are using the In memory repository or the actual repository
            if (Boolean.Parse(Configuration["ServiceOptions:UseInMemoryRepository"]))
            {
                // Use the In Memory option
                services.AddScoped<IPrismRepository, MemoryPrismRepository>();
            }
            else
            {
                services.AddScoped<IDBFactory, DbFactory>(sp => {
                    var auth = Configuration["ServiceOptions:AuthenticationDBConnectionString"];
                    var prism = Configuration["ServiceOptions:PrismDBConnectionString"];

                    return new DbFactory(auth, prism);
                });
                // Use the actual DB
                services.AddScoped<IPrismRepository, NPocoPrismRepository>();
            }

            // Configure the Safe Service
            services.AddScoped<IPrismService, PrismService>();

            // Configure the Authentication Service
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Configure Identity Stores
            services.AddTransient<IUserStore<ApplicationUser>, CustomUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, CustomRoleStore>();
            services.AddTransient<IUserClaimStore<ApplicationUser>, CustomClaimStore>();

            // Conifgure Lockout options
            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

            // Configure Identity Context
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Configure password requirement options
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            }).AddDefaultTokenProviders();

            // Configure Sign In options
            services.Configure<IdentityOptions>(options =>
            {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                //options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            // Configure User options
            services.Configure<IdentityOptions>(options =>
            {
                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

            });

            // Configures the Application Cookie options
            // *MUST* be after services.AddIdentity
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Shared/AccessDeniedView";
                options.Cookie.Name = "uvimcoprosim.com";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                //options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            // Add the Authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdminPolicy", policy => policy.RequireAssertion(context => new Policies().SuperAdminPolicyAccess(context)));
                options.AddPolicy("AdminPolicy", policy => policy.RequireAssertion(context => new Policies().AdminPolicyAccess(context)));
                options.AddPolicy("SubAdminPolicy", policy => policy.RequireAssertion(context => new Policies().SubAdminPolicyAccess(context)));
                options.AddPolicy("UserPolicy", policy => policy.RequireAssertion(context => new Policies().UserPolicyAccess(context)));
                options.AddPolicy("ManageRolesPolicy", policy => policy.RequireAssertion(context => new Policies().ManageRolesPolicyAccess(context)));
                options.AddPolicy("CreateRolePolicy", policy => policy.RequireAssertion(context => new Policies().CreateRolePolicyAccess(context)));
                options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context => new Policies().EditRolePolicyAccess(context)));
                options.AddPolicy("DeleteRolePolicy", policy => policy.RequireAssertion(context => new Policies().DeleteRolePolicyAccess(context)));
                options.AddPolicy("ManageUsersPolicy", policy => policy.RequireAssertion(context => new Policies().ManageUsersPolicyAccess(context)));
                options.AddPolicy("CreateUserPolicy", policy => policy.RequireAssertion(context => new Policies().CreateUserPolicyAccess(context)));
                options.AddPolicy("EditUserPolicy", policy => policy.RequireAssertion(context => new Policies().EditUserPolicyAccess(context)));
                options.AddPolicy("DeleteUserPolicy", policy => policy.RequireAssertion(context => new Policies().DeleteUserPolicyAccess(context)));
                options.AddPolicy("ManageUserRolesPolicy", policy => policy.RequireAssertion(context => new Policies().ManageUserRolesPolicyAccess(context))
                                                                            .AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

            });

            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();

            // Configure MVC Service
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddXmlSerializerFormatters()
            .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddRazorPages()
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Reports/ReportsView", "/Reports/");
                options.Conventions.AddPageRoute("/Reports/ReportIFrameView", "/ReportsIFrameView/");
                options.Conventions.AddPageRoute("/Reports/ReportDataView", "/ReportsDataView/{GUID}/{handler?}");
                //options.Conventions.AddPageRoute("/Privacy", "/Privacy");
                //options.Conventions.AddPageRoute("/About", "/About/");
            });
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

    static class ServiceCollectionExtensions
    {
        // Adds all Http client services (like Service-Agents) using resilient Http requests based on HttpClient factory and Polly's policies 
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register delegating handlers
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddTransient<HttpClientRequestIdDelegatingHandler>();

            //set 5 min as the lifetime for each HttpMessageHandler int the pool
            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));

            //add http client services
            //services.AddHttpClient<ILocalPrismService, LocalPrismService>()
            //       .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
            //       .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            //add http client services
            services.AddHttpClient<IPrismService, PrismService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            //add custom application services
            //services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();

            return services;
        }
    }
}
