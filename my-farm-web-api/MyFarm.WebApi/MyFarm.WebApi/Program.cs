using System.Reflection;
using MyFarm.WebApi;
using MyFarm.WebApi.Extensions;
using Serilog;
using Serilog.Exceptions;

public static class Program
{
    private static string _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? nameof(MyFarm.WebApi);
    private static IConfiguration _config;

    public static async Task Main(string[] args)
    {
        var builder = CreateHostBuilder(args);
        var host = builder.Build();

        await host.PrepareAndRunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json",
                optional: false,
                reloadOnChange: true)
            .AddJsonFile($"appsettings.{_environmentName}.json",
                optional: true,
                reloadOnChange: true)
            .AddEnvironmentVariables();

        if (args.Length != 0)
        {
            configurationBuilder = configurationBuilder.AddCommandLine(args);
        }

        _config = configurationBuilder.Build();

        var hostBuilder = Host.CreateDefaultBuilder(args)
            .UseEnvironment(_environmentName)
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddConfiguration(_config);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseConfiguration(_config)
                    .UseStartup<Startup>();
            })
            .ConfigureLogging(log =>
            {
                log.SetMinimumLevel(LogLevel.Debug);
            })
            .UseSerilog();

        return hostBuilder;
    }

    private static async Task PrepareAndRunAsync(this IHost host, CancellationToken token = default)
    {
        try
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();

                ConfigureLogging(configuration);

                Log.Information("Using {EnvironmentName} environment for app", _environmentName);
            }

            await host.RunAsync(token);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Failed to start {AssemblyName}", GetExecutingAssemblyName());
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static LoggerConfiguration ConfigureLogging(IConfiguration configuration)
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
            .ReadFrom.Configuration(configuration);

        Log.Logger = logConfiguration.CreateLogger();
        Log.Verbose("Using {EnvironmentName} environment for logging", _environmentName);

        return logConfiguration;
    }

    private static string GetExecutingAssemblyName() => Assembly.GetExecutingAssembly().GetName().Name;
}
