namespace Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SpectreConsole;

public static partial class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.SpectreConsole(
                "{Timestamp:HH:mm:ss} [{Level:u4}] {Message:lj}{NewLine}{Exception}",
                LogEventLevel.Information)
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }
}
