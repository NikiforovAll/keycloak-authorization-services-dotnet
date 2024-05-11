namespace OpenTelemetry.Metrics;

/// <summary>
/// Extension methods to simplify registering of ASP.NET Core request instrumentation.
/// </summary>
public static class MeterProviderBuilderExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="builder"><see cref="MeterProviderBuilder"/> being configured.</param>
    /// <returns>The instance of <see cref="MeterProviderBuilder"/> to chain the calls.</returns>
    public static MeterProviderBuilder AddKeycloakAuthServicesInstrumentation(
        this MeterProviderBuilder builder
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.ConfigureMeters();
    }

    internal static MeterProviderBuilder ConfigureMeters(this MeterProviderBuilder builder) =>
        builder
            .AddMeter("Keycloak.AuthServices.Authorization");
}
