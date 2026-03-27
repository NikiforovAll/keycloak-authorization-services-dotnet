namespace Keycloak.AuthServices.Authorization.TokenIntrospection;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
/// Claims transformation that enriches a <see cref="ClaimsPrincipal"/> with claims
/// resolved from the Keycloak token introspection endpoint.
/// Designed for use with lightweight access tokens that lack business claims.
/// </summary>
public class KeycloakTokenIntrospectionClaimsTransformation
    : IKeycloakTokenIntrospectionTransformation
{
    private static readonly HashSet<string> SkipClaims = new(StringComparer.OrdinalIgnoreCase)
    {
        "active",
        "iat",
        "exp",
        "nbf",
        "iss",
        "sub",
        "aud",
        "jti",
        "typ",
        "azp",
        "auth_time",
        "session_state",
        "acr",
        "sid",
    };

    // Claims with complex JSON values that should be stored as serialized JSON strings
    private static readonly HashSet<string> JsonClaims = new(StringComparer.OrdinalIgnoreCase)
    {
        "resource_access",
        "realm_access",
        "organization",
    };

    private readonly IKeycloakTokenIntrospectionClient introspectionClient;
    private readonly HybridCache cache;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IOptions<KeycloakTokenIntrospectionOptions> options;
    private readonly ILogger<KeycloakTokenIntrospectionClaimsTransformation> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakTokenIntrospectionClaimsTransformation"/> class.
    /// </summary>
    public KeycloakTokenIntrospectionClaimsTransformation(
        IKeycloakTokenIntrospectionClient introspectionClient,
        HybridCache cache,
        IHttpContextAccessor httpContextAccessor,
        IOptions<KeycloakTokenIntrospectionOptions> options,
        ILogger<KeycloakTokenIntrospectionClaimsTransformation> logger
    )
    {
        this.introspectionClient = introspectionClient;
        this.cache = cache;
        this.httpContextAccessor = httpContextAccessor;
        this.options = options;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        ArgumentNullException.ThrowIfNull(principal);

        if (principal.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            return principal;
        }

        // Skip if key claims are already present (not a lightweight token)
        if (
            principal.FindFirst("resource_access") is not null
            || principal.FindFirst("realm_access") is not null
        )
        {
            return principal;
        }

        var httpContext = this.httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return principal;
        }

        // Extract token from Authorization header directly.
        // Cannot use GetTokenAsync() — it calls AuthenticateAsync which triggers TransformAsync again (infinite recursion).
        var authHeader = httpContext.Request.Headers.Authorization.ToString();
        var token = authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..]
            : null;
        if (string.IsNullOrEmpty(token))
        {
            return principal;
        }

        var cacheKey = GetCacheKey(principal, token);

        this.logger.LogIntrospectingToken(principal.Identity?.Name);

        try
        {
            var opts = this.options.Value;
            var cacheOptions = new HybridCacheEntryOptions { Expiration = opts.CacheDuration };

            var response = await this.cache.GetOrCreateAsync(
                cacheKey,
                async ct => await this.introspectionClient.IntrospectTokenAsync(token, ct),
                cacheOptions
            );

            if (response is null)
            {
                return principal;
            }

            if (!response.Active)
            {
                this.logger.LogTokenNotActive(principal.Identity?.Name);
                return principal;
            }

            var claimsAdded = EnrichPrincipal(identity, response.Claims);
            this.logger.LogTokenIntrospectionEnrichedClaims(claimsAdded);

            opts.OnTokenIntrospected?.Invoke(identity, response.Claims);
        }
        catch (Exception ex)
        {
            this.logger.LogTokenIntrospectionFailed(ex);
        }

        return principal;
    }

    private static string GetCacheKey(ClaimsPrincipal principal, string token)
    {
        var jti = principal.FindFirst("jti")?.Value;
        if (!string.IsNullOrEmpty(jti))
        {
            return $"keycloak:introspect:{jti}";
        }

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return $"keycloak:introspect:{Convert.ToHexStringLower(hash)}";
    }

    private static int EnrichPrincipal(
        ClaimsIdentity identity,
        Dictionary<string, JsonElement> claims
    )
    {
        var count = 0;
        foreach (var (key, value) in claims)
        {
            if (SkipClaims.Contains(key))
            {
                continue;
            }

            // Don't add claims that already exist
            if (identity.FindFirst(key) is not null)
            {
                continue;
            }

            if (JsonClaims.Contains(key))
            {
                identity.AddClaim(new Claim(key, value.GetRawText(), "JSON"));
                count++;
            }
            else if (value.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in value.EnumerateArray())
                {
                    identity.AddClaim(new Claim(key, item.GetString() ?? item.GetRawText()));
                    count++;
                }
            }
            else if (value.ValueKind == JsonValueKind.Object)
            {
                identity.AddClaim(new Claim(key, value.GetRawText(), "JSON"));
                count++;
            }
            else
            {
                identity.AddClaim(new Claim(key, value.ToString()));
                count++;
            }
        }

        return count;
    }
}
