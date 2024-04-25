namespace Keycloak.AuthServices.Common;

using Microsoft.Extensions.Configuration;

/// <summary>
/// Allows to use Keycloak configuration in a original format (kebab-case)
/// </summary>
public static class KeycloakFormatBinder
{
    /// <summary>
    /// Gets the default binder for handling kebab-case configuration format used by Keycloak.
    /// </summary>
    public static Action<BinderOptions> Instance { get; } =
        options => options.BindNonPublicProperties = true;
}
