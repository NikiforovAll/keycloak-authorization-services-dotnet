namespace Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SpectreConsole;

public static partial class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection.KeyManagement", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authorization", LogEventLevel.Verbose)
            .MinimumLevel.Override("System.Net.Http", LogEventLevel.Debug)
            .MinimumLevel.Override("Keycloak.AuthServices", LogEventLevel.Verbose)
            .WriteTo.SpectreConsole(
                "{SourceContext}{NewLine}{Timestamp:HH:mm:ss} [{Level:u4}] {Message:lj}{NewLine}{Exception}",
                LogEventLevel.Verbose)
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }
}
