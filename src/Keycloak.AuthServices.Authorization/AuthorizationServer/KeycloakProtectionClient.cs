namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Keycloak.AuthServices.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <inheritdoc />
public class KeycloakProtectionClient : IKeycloakProtectionClient
{
    private readonly HttpClient httpClient;
    private readonly IOptions<KeycloakProtectionClientOptions> options;
    private readonly ILogger<KeycloakProtectionClient> logger;

    /// <summary>
    /// Constructs KeycloakProtectionClient
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="clientOptions"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public KeycloakProtectionClient(
        HttpClient httpClient,
        IOptions<KeycloakProtectionClientOptions> clientOptions,
        ILogger<KeycloakProtectionClient> logger
    )
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.options = clientOptions;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        ScopesValidationMode? scopesValidationMode = default,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(scope);

        var data = this.PrepareData(resource, scope);

        var response = await this.httpClient.PostAsync(
            KeycloakConstants.TokenEndpointPath,
            new FormUrlEncodedContent(data),
            cancellationToken
        );

        return await this.HandleResponse(
            response,
            resource,
            scope,
            scopesValidationMode,
            cancellationToken
        );
    }

    private Dictionary<string, string> PrepareData(string resource, string scope)
    {
        var permission = string.IsNullOrWhiteSpace(scope) ? resource : $"{resource}#{scope}";
        var audience = this.options.Value.Resource;

        return new Dictionary<string, string>
        {
            { "grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket" },
            { "response_mode", scope.Contains(',') ? "permissions" : "decision" },
            { "audience", audience ?? string.Empty },
            { "permission", permission }
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
            return await this.HandleErrorResponse(response, cancellationToken);
        }

        var scopes = scope.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        if (scopes is { Count: <= 1 })
        {
            return true;
        }

        var scopeResponse = await response.Content.ReadFromJsonAsync<ScopeResponse[]?>(
            cancellationToken: cancellationToken
        );

        return this.ValidateScopes(scopeResponse, resource, scopes, scopesValidationMode);
    }

    private async Task<bool> HandleErrorResponse(
        HttpResponseMessage response,
        CancellationToken cancellationToken
    )
    {
        var error = await response.Content.ReadFromJsonAsync<ErrorResponse?>(
            cancellationToken: cancellationToken
        );

        if (
            !string.IsNullOrWhiteSpace(error?.Error)
            && error.Error != ErrorResponse.AccessDeniedError
        )
        {
#pragma warning disable CA1848 // Use the LoggerMessage delegates
            this.logger.LogWarning(
                "Issues invoking {Method} - {Errors}",
                nameof(VerifyAccessToResource),
                error
            );
#pragma warning restore CA1848 // Use the LoggerMessage delegates
        }

        return false;
    }

    private bool ValidateScopes(
        ScopeResponse[]? scopeResponse,
        string resource,
        List<string> scopes,
        ScopesValidationMode? scopesValidationMode
    )
    {
        var resourceToValidate =
            Array.Find(
                scopeResponse ?? Array.Empty<ScopeResponse>(),
                r => string.Equals(r.Rsname, resource, StringComparison.Ordinal)
            ) ?? throw new KeycloakException($"Unable to find a resource - {resource}");

        scopesValidationMode ??= this.options.Value.ScopesValidationMode;

        if (scopesValidationMode == ScopesValidationMode.AllOf)
        {
            var resourceScopes = resourceToValidate.Scopes;
            var allScopesPresent = scopes.TrueForAll(s => resourceScopes.Contains(s));

            return allScopesPresent;
        }
        else if (scopesValidationMode == ScopesValidationMode.AnyOf)
        {
            var resourceScopes = resourceToValidate.Scopes;
            var anyScopePresent = scopes.Exists(s => resourceScopes.Contains(s));

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
