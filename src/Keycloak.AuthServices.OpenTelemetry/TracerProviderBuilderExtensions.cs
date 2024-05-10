namespace OpenTelemetry.Trace;

/// <summary>
/// Provides extension methods for configuring Keycloak AuthServices instrumentation on the <see cref="TracerProviderBuilder"/>.
/// </summary>
public static class TracerProviderBuilderExtensions
{
    /// <summary>
    /// Adds Keycloak AuthServices instrumentation to the <see cref="TracerProviderBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="TracerProviderBuilder"/> being configured.</param>
    /// <returns>The instance of <see cref="TracerProviderBuilder"/> to chain the calls.</returns>

    public static TracerProviderBuilder AddKeycloakAuthServicesInstrumentation(
        this TracerProviderBuilder builder
    )
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.AddSource("Keycloak.AuthServices.Authorization");

        return builder;
    }
}
