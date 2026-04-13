namespace Keycloak.AuthServices.Sdk;

using Microsoft.Extensions.Options;

/// <summary>
/// Validates <see cref="KeycloakProtectionClientOptions"/> at startup.
/// Ensures the minimum required configuration is present before any Protection API calls are made.
/// </summary>
public class KeycloakProtectionClientOptionsValidator
    : IValidateOptions<KeycloakProtectionClientOptions>
{
    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, KeycloakProtectionClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (!Uri.TryCreate(options.AuthServerUrl, UriKind.Absolute, out _))
        {
            return ValidateOptionsResult.Fail(
                "Keycloak Protection HTTP client requires a valid absolute URI for 'AuthServerUrl'."
            );
        }

        if (string.IsNullOrWhiteSpace(options.Realm))
        {
            return ValidateOptionsResult.Fail(
                "Keycloak Protection HTTP client requires 'Realm' to be configured."
            );
        }

        return ValidateOptionsResult.Success;
    }
}
