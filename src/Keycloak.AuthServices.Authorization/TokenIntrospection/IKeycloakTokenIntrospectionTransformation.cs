namespace Keycloak.AuthServices.Authorization.TokenIntrospection;

using Microsoft.AspNetCore.Authentication;

/// <summary>
/// Marker interface for the token introspection claims transformation.
/// Allows the authorization composite to discover and chain the introspection
/// transformation without depending on the concrete implementation.
/// </summary>
public interface IKeycloakTokenIntrospectionTransformation : IClaimsTransformation;
