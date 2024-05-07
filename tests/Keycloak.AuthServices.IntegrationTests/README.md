# Integration Tests

## Sync Realm Config

Inside docker container run:

```bash
/opt/keycloak/bin/kc.sh export --dir /opt/keycloak/data/import --realm Test
```

## Test

```bash
dotnet test \
    --logger:"console;verbosity=detailed" \
    --filter NAME 
```

## User Registry in Test Realm

```csharp
public static class TestUsersRegistry
{
    public static TestUser Admin =
        new(
            UserName: "testadminuser",
            Password: "test",
            Email: "testadminuser@test.com",
            RealmRoles: ["Admin"],
            ClientRoles: []
        );

    public static TestUser Tester =
        new(
            UserName: "test",
            Password: "test",
            Email: "test@test.com",
            RealmRoles: ["Reader"],
            ClientRoles: new() { ["test-client"] = ["TestClientRole"] }
        );
}
```
