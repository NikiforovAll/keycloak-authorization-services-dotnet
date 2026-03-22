# Protection API SDK

The Protection API provides UMA-compliant endpoints for:

- **Resource Management** — CRUD operations on protected resources
- **Permission Management** — create permission tickets, manage permission state
- **Policy API** — manage permissions on behalf of users

## Setup

```bash
dotnet add package Keycloak.AuthServices.Sdk
```

```csharp
builder.Services.AddKeycloakProtectionHttpClient(builder.Configuration);
```

Registers `IKeycloakProtectionClient` integrated with `IHttpClientFactory`.

Returns `IHttpClientBuilder` for further configuration:

```csharp
builder.Services
    .AddKeycloakProtectionHttpClient(builder.Configuration)
    .AddStandardResilienceHandler();
```

## Access Token

Protection API is protected — requires authentication. Use the same token management approach as Admin SDK:

```csharp
builder.Services
    .AddKeycloakProtectionHttpClient(builder.Configuration)
    .AddClientCredentialsTokenHandler("keycloak");
```

## Available Operations

### Resource Management

```csharp
// List resources
var resources = await protectionClient.GetResourcesAsync("my-realm");

// Get resource by ID
var resource = await protectionClient.GetResourceAsync("my-realm", resourceId);

// Create resource
await protectionClient.CreateResourceAsync("my-realm", new Resource
{
    Name = "my-resource",
    Type = "urn:my-type",
    Scopes = new[] { new Scope { Name = "read" }, new Scope { Name = "write" } }
});
```

### Policy Management

```csharp
// List policies
var policies = await protectionClient.GetPoliciesAsync("my-realm");

// Get policy by ID
var policy = await protectionClient.GetPolicyAsync("my-realm", policyId);
```

## Distinction: Protection API vs Authorization Server

| Feature | Protection API (`IKeycloakProtectionClient`) | Authorization Server (`IAuthorizationServerClient`) |
|---------|----------------------------------------------|-----------------------------------------------------|
| Purpose | Manage resources, permissions, policies | Evaluate permissions at runtime |
| Package | `Keycloak.AuthServices.Sdk` | `Keycloak.AuthServices.Authorization` |
| Used by | Backend services managing Keycloak config | Web APIs protecting endpoints |
| Auth | Service account token | User's access token (header propagation) |
