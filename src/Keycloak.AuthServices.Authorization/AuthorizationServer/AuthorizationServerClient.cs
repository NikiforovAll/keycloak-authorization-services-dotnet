namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Keycloak.AuthServices.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <inheritdoc />
/// <summary>
/// </summary>
/// <param name="httpClient"></param>
/// <param name="clientOptions"></param>
/// <param name="logger"></param>
/// <exception cref="ArgumentNullException"></exception>
public class AuthorizationServerClient(
    HttpClient httpClient,
    IOptions<KeycloakAuthorizationServerOptions> clientOptions,
    ILogger<AuthorizationServerClient> logger
) : IAuthorizationServerClient
{
    private readonly HttpClient httpClient =
        httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly IOptions<KeycloakAuthorizationServerOptions> options = clientOptions;
    private readonly ILogger<AuthorizationServerClient> logger = logger;

    /// <inheritdoc />
    public async Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        ScopesValidationMode? scopesValidationMode = default,
        string? audience = null,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(scope);

        this.logger.LogVerifyingAccess(resource, scope);

        return await this.SendVerificationRequestAsync(
            resource,
            scope,
            accessToken: null,
            audience,
            scopesValidationMode,
            cancellationToken
        );
    }

    /// <inheritdoc />
    public async Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        string accessToken,
        ScopesValidationMode? scopesValidationMode = default,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(scope);
        ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);

        this.logger.LogVerifyingAccessWithTokenOverride(resource, scope);

        return await this.SendVerificationRequestAsync(
            resource,
            scope,
            accessToken,
            audience: null,
            scopesValidationMode,
            cancellationToken
        );
    }

    private async Task<bool> SendVerificationRequestAsync(
        string resource,
        string scope,
        string? accessToken,
        string? audience,
        ScopesValidationMode? scopesValidationMode,
        CancellationToken cancellationToken
    )
    {
        try
        {
            using var content = new FormUrlEncodedContent(
                this.PrepareRequest(resource, scope, audience)
            );
            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                KeycloakConstants.TokenEndpointPath
            )
            {
                Content = content,
            };

            if (accessToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    this.options.Value.SourceAuthenticationScheme,
                    accessToken
                );
            }

            using var response = await this.httpClient.SendAsync(request, cancellationToken);

            return await this.HandleResponse(
                response,
                resource,
                scope,
                scopesValidationMode,
                cancellationToken
            );
        }
        catch (Exception exception)
        {
            this.logger.LogVerifyAccessToResourceFailed(exception, resource, scope);
            throw;
        }
    }

    private Dictionary<string, string> PrepareRequest(
        string resource,
        string scope,
        string? audience = null
    )
    {
        var permission = string.IsNullOrWhiteSpace(scope) ? resource : $"{resource}#{scope}";
        var resolvedAudience = audience ?? this.options.Value.Resource ?? string.Empty;
        var responseMode = scope.Contains(',') ? "permissions" : "decision";

        return new Dictionary<string, string>
        {
            { "grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket" },
            { "response_mode", responseMode },
            { "audience", resolvedAudience },
            { "permission", permission },
        };
    }

    private async Task<bool> HandleResponse(
        HttpResponseMessage response,
        string resource,
        string scope,
        ScopesValidationMode? scopesValidationMode,
        CancellationToken cancellationToken
    )
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!error.Contains(ErrorResponse.AccessDeniedError))
            {
                this.logger.LogUnableToRecognizeResponse(resource, error);
            }

            return false;
        }

        return await this.ValidateScopesAsync(
            resource,
            scope,
            scopesValidationMode,
            response,
            cancellationToken
        );
    }

    private async Task<bool> ValidateScopesAsync(
        string resource,
        string scope,
        ScopesValidationMode? scopesValidationMode,
        HttpResponseMessage response,
        CancellationToken cancellationToken
    )
    {
        var scopes = scope.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        if (scopes is { Count: <= 1 })
        {
            return true;
        }

        var scopeResponse = await response.Content.ReadFromJsonAsync<ScopeResponse[]?>(
            cancellationToken: cancellationToken
        );

        return this.ValidateScopesAsync(resource, scopes, scopeResponse, scopesValidationMode);
    }

    private bool ValidateScopesAsync(
        string resource,
        List<string> scopesToValidate,
        ScopeResponse[]? scopeResponse,
        ScopesValidationMode? scopesValidationMode
    )
    {
        scopeResponse ??= Array.Empty<ScopeResponse>();

        var resourceToValidate = Array.Find(
            scopeResponse,
            r => string.Equals(r.Rsname, resource, StringComparison.Ordinal)
        );

        if (resourceToValidate is null)
        {
            throw new KeycloakException($"Unable to find a resource - {resource}");
        }

        scopesValidationMode ??= this.options.Value.ScopesValidationMode;

        this.logger.LogValidatingScopes(resource, scopesValidationMode.Value);

        if (scopesValidationMode == ScopesValidationMode.AllOf)
        {
            var resourceScopes = resourceToValidate.Scopes;
            var allScopesPresent = scopesToValidate.TrueForAll(s => resourceScopes.Contains(s));

            return allScopesPresent;
        }
        else if (scopesValidationMode == ScopesValidationMode.AnyOf)
        {
            var resourceScopes = resourceToValidate.Scopes;
            var anyScopePresent = scopesToValidate.Exists(s => resourceScopes.Contains(s));

            return anyScopePresent;
        }

        return true;
    }

    private sealed record ScopeResponse(string Rsid, string Rsname, List<string> Scopes);

    private sealed record ErrorResponse
    {
        public const string AccessDeniedError = "access_denied";

        [JsonPropertyName("error")]
        public string Error { get; init; } = default!;

        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; init; } = default!;
    }
}
