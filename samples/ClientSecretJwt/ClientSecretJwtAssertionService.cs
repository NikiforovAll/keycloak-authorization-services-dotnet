namespace ClientSecretJwt;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Duende.AccessTokenManagement;
using Duende.IdentityModel;
using Duende.IdentityModel.Client;
using Keycloak.AuthServices.Sdk;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Implements Duende's <see cref="IClientAssertionService"/> for the
/// <c>client_secret_jwt</c> authentication method (RFC 7523 / Keycloak "Signed JWT with Client Secret").
///
/// Instead of sending the shared secret over the wire, this service builds a short-lived
/// HS256-signed JWT (<c>client_assertion</c>). Keycloak verifies the signature using the
/// same shared secret it already holds — the secret itself never leaves the application.
/// </summary>
public sealed class ClientSecretJwtAssertionService(IOptions<KeycloakAdminClientOptions> options)
    : IClientAssertionService
{
    private readonly KeycloakAdminClientOptions _options = options.Value;

    public Task<ClientAssertion?> GetClientAssertionAsync(
        ClientCredentialsClientName? clientName = null,
        TokenRequestParameters? parameters = null,
        CancellationToken ct = default
    )
    {
        var clientId = _options.Resource;
        var tokenEndpoint = _options.KeycloakTokenEndpoint;
        var secret = _options.Credentials.Secret;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken(
            issuer: clientId,
            audience: tokenEndpoint,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, clientId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ],
            notBefore: now,
            expires: now.AddMinutes(1),
            signingCredentials: credentials
        );

        return Task.FromResult<ClientAssertion?>(
            new ClientAssertion
            {
                Type = OidcConstants.ClientAssertionTypes.JwtBearer,
                Value = new JwtSecurityTokenHandler().WriteToken(token),
            }
        );
    }
}
