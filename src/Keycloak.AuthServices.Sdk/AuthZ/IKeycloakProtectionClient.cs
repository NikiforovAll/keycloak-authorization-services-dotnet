namespace Keycloak.AuthServices.Sdk.Admin;

public interface IKeycloakProtectionClient
{
    Task<bool> VerifyAccessToResource(string resource, string scope, CancellationToken cancellationToken);
}
