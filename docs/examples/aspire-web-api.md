# Aspire + Web API

This samples contains Keycloak installation configured via configuration files.

Here is what it does:

1. Starts a Keycloak Instance as part of Aspire Integration
2. Imports realm and test users (`test1:test`, `test2:test`)

The Keycloak is already configured, all you need to do is to run sample and try to retrieve token via Swagger UI.

Run:

```bash
dotnet run --project ./AppHost
```

## Code

`AppHost`:

<<< @/../samples/GettingStartedAndAspire/AppHost/Program.cs

`Api`:

<<< @/../samples/GettingStartedAndAspire/Api/Program.cs

See sample source code: [keycloak-authorization-services-dotnet/tree/main/samples/GettingStartedAndAspire](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/GettingStartedAndAspire)
