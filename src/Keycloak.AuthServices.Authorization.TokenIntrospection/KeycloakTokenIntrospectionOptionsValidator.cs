namespace Keycloak.AuthServices.Authorization.TokenIntrospection;

using Microsoft.Extensions.Options;

/// <summary>
/// Validates <see cref="KeycloakTokenIntrospectionOptions"/> at startup.
/// Ensures client credentials are configured when introspection is enabled.
/// </summary>
public class KeycloakTokenIntrospectionOptionsValidator
    : IValidateOptions<KeycloakTokenIntrospectionOptions>
{
    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, KeycloakTokenIntrospectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.AuthServerUrl))
        {
            return ValidateOptionsResult.Fail(
                "Keycloak token introspection requires 'AuthServerUrl' to be configured."
            );
        }

        if (string.IsNullOrWhiteSpace(options.Realm))
        {
            return ValidateOptionsResult.Fail(
                "Keycloak token introspection requires 'Realm' to be configured."
            );
        }

        if (string.IsNullOrWhiteSpace(options.Resource))
        {
            return ValidateOptionsResult.Fail(
                "Keycloak token introspection requires 'Resource' (client_id) to be configured."
            );
        }

        if (string.IsNullOrWhiteSpace(options.Credentials?.Secret))
        {
            return ValidateOptionsResult.Fail(
                "Keycloak token introspection requires 'Credentials:Secret' (client_secret) to be configured. "
                    + "Token introspection requires a confidential client."
            );
        }

        return ValidateOptionsResult.Success;
    }
}
