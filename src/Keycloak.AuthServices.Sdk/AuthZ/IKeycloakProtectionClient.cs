namespace Keycloak.AuthServices.Sdk.AuthZ;

public interface IKeycloakProtectionClient
{
    Task<bool> VerifyAccessToResource(string resource, string scope, CancellationToken cancellationToken);
}
