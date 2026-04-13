namespace Keycloak.AuthServices.Sdk;

using Microsoft.Extensions.Options;

/// <summary>
/// Validates <see cref="KeycloakAdminClientOptions"/> at startup.
/// Ensures the minimum required configuration is present before any Admin API calls are made.
/// </summary>
public class KeycloakAdminClientOptionsValidator : IValidateOptions<KeycloakAdminClientOptions>
{
    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, KeycloakAdminClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (!Uri.TryCreate(options.AuthServerUrl, UriKind.Absolute, out _))
        {
            return ValidateOptionsResult.Fail(
                "Keycloak Admin HTTP client requires a valid absolute URI for 'AuthServerUrl'."
            );
        }

        if (string.IsNullOrWhiteSpace(options.Realm))
        {
            return ValidateOptionsResult.Fail(
                "Keycloak Admin HTTP client requires 'Realm' to be configured."
            );
        }

        return ValidateOptionsResult.Success;
    }
}
