namespace Keycloak.AuthServices.Sdk.Admin.Constants;

public static class KeycloakClientApiConstants
{
    private const string AdminApiBase = "/admin";

    private const string Realms = "realms";

    public const string GetRealm = $"{AdminApiBase}/{Realms}/{{realm}}";
}
