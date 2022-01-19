namespace Keycloak.AuthServices.Authentication.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

/// <summary>
/// Keycloak configuration provider
/// </summary>
public class KeycloakConfigurationSource : JsonConfigurationSource
{
    /// <summary>
    /// Builds the <see cref="JsonConfigurationProvider"/> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
    /// <returns>A <see cref="JsonConfigurationProvider"/></returns>
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        this.EnsureDefaults(builder);
        return new KeycloakConfigurationProvider(this);
    }
}
