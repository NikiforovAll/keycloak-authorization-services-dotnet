namespace Keycloak.AuthServices.IntegrationTests;

public static class TestUsers
{
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable IDE1006 // Naming Styles
    public static TestUser Admin =
        new(
            UserName: "testadminuser",
            Password: "test",
            Email: "testadminuser@test.com",
            RealmRoles: [KeycloakRoles.Admin],
            ClientRoles: []
        );

    public static TestUser Tester =
        new(
            UserName: "test",
            Password: "test",
            Email: "test@test.com",
            RealmRoles: [KeycloakRoles.Reader],
            ClientRoles: new Dictionary<string, string[]>
            {
                ["test-client"] = [KeycloakRoles.TestClientRole]
            }
        );
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CA2211 // Non-constant fields should not be visible
}

public static class KeycloakRoles
{
    public const string Admin = "Admin";
    public const string Reader = "Admin";
    public const string TestClientRole = "TestClientRole";
}

public record TestUser(
    string UserName,
    string Password,
    string Email,
    string[] RealmRoles,
    Dictionary<string, string[]> ClientRoles
);
