namespace Api;

using Serilog;
using Serilog.Events;

public static class ApplicationLogger
{
    public static void ConfigureLogger(this ConfigureHostBuilder host)
    {
#pragma warning disable CA1305 // Specify IFormatProvider
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(
                outputTemplate: "[{Level:u4}] |{SourceContext,30}({EventId})| {Message:lj}{NewLine}{Exception}",
                restrictedToMinimumLevel: LogEventLevel.Debug
            )
            .CreateBootstrapLogger();
#pragma warning restore CA1305 // Specify IFormatProvider

        host.UseSerilog();
    }
}
