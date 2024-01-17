using Microsoft.AspNetCore.HttpOverrides;
using MyFarm.Core;
using MyFarm.Log;
using MyFarm.WebApi.Extensions;
using MyFarm.WebApi.Services;
using Serilog;
using Serilog.Exceptions;

namespace MyFarm.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        private static string _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? nameof(MyFarm.WebApi);

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _configuration = config;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services
                   .AddControllersWithViews()
                   .AddNewtonsoftJson(options =>
                       options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc);

            services
                .AddDbContexts()
                .AddAutoMapper()
                .AddLogger()
                .AddCoreServices()
                .AddWebApiServices()
                .AddResponseCompression()
                .AddHttpContextAccessor()
                .AddSingleton(ConfigureLogging())
                .AddFarmCorsPolicy();
        }

        public void Configure(IApplicationBuilder app, AppInitializer appInitializer) 
        {
            var isLocal = _environmentName.ToLower().Contains("production");

            if (isLocal)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("FarmPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            appInitializer.SeedAsync().GetAwaiter().GetResult();
        }

        private Serilog.ILogger ConfigureLogging()
        {
            var logConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", _environmentName)
                .Enrich.With(new ExceptionEnricher())
                .Enrich.With(new MessageEnricher())
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .ReadFrom.Configuration(_configuration);

            var logger = logConfiguration.CreateLogger();
            Serilog.Log.Verbose("Using {EnvironmentName} environment for logging", _environmentName);
            
            return logger;
        }

    }
}
