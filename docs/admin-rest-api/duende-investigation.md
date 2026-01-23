# Duende.AccessTokenManagement Investigation Results

**Issue**: [#188](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/issues/188) - Duende.AccessTokenManagement is archived

**Investigation Date**: January 2026

## Executive Summary

The Duende.AccessTokenManagement package repository was archived on **November 19, 2024**, but the project has been **migrated to the DuendeSoftware/foss monorepo** where active development continues. The package remains production-ready and is the recommended solution for OAuth/OIDC token management in this project.

**Recommendation**: ✅ **No migration required** - Continue using Duende.AccessTokenManagement

---

## Current Status of Duende.AccessTokenManagement

### Package Status ✅ Active

- **Original Repository**: `DuendeSoftware/Duende.AccessTokenManagement` (archived Nov 19, 2024)
- **New Repository**: [DuendeSoftware/foss](https://github.com/DuendeSoftware/foss/tree/main/access-token-management) (active)
- **Current Version in Project**: 4.1.0 (latest, released December 2024)
- **License**: Apache 2.0 (permissive, free for commercial use)
- **Supported Frameworks**: .NET 8, .NET 9
- **Status**: Actively maintained with ongoing development

### Why the Repository Was Archived

DuendeSoftware consolidated multiple projects into a single FOSS (Free and Open Source Software) monorepo. This is a common practice in modern software development and does **not** indicate the project is abandoned.

---

## Current Usage in This Project

### Dependencies

**Packages** (`Directory.Packages.props`):
- `samples/Directory.Packages.props` - Version 4.1.0
- `tests/Directory.Packages.props` - Version 4.1.0

### Code Usage

1. **Sample Application** (`samples/ResourceAuthorization/Program.cs`):
   - Client credentials token management for Keycloak Admin API
   - Client credentials token management for Keycloak Protection API
   - Uses `AddClientCredentialsTokenManagement()` and `AddClientCredentialsTokenHandler()`

2. **Integration Tests** (`tests/Keycloak.AuthServices.IntegrationTests/`):
   - `Admin/KeycloakRealmClientTests.cs` - Admin API token management tests
   - `AdminKiota/KeycloakRealmKiotaClientTests.cs` - Kiota client tests
   - `Protection/KeycloakProtectionClientTests.cs` - Protection API tests
   - `Utils.cs` - Helper methods for token management setup

### Documentation References

- `docs/admin-rest-api/access-token.md` - Recommends Duende for token management
- `src/Keycloak.AuthServices.Sdk/README.md` - Notes token retrieval handled by 3rd parties

---

## Alternative Solutions Evaluated

### Option 1: Continue with Duende.AccessTokenManagement ✅ **Recommended**

**Pros**:
- ✅ No code changes required
- ✅ Currently on latest version (4.1.0)
- ✅ Actively maintained in FOSS repository
- ✅ Battle-tested, production-ready
- ✅ Apache 2.0 license (free for all use cases)
- ✅ Supports generic OAuth/OIDC providers (including Keycloak)
- ✅ Automatic token caching and refresh
- ✅ Supports .NET 8 and .NET 9

**Cons**:
- ⚠️ Repository location changed (documentation update needed)
- ⚠️ Minor confusion from archived status

**Effort**: Minimal (documentation updates only)

---

### Option 2: Microsoft.Identity.Web ❌ **Not Recommended**

**Description**: Official Microsoft package for Azure AD/Microsoft Identity Platform

**Pros**:
- Official Microsoft support
- Well-documented
- Long-term support guaranteed

**Cons**:
- ❌ **Incompatible**: Designed for Azure AD/Microsoft Identity Platform only
- ❌ **Not suitable for Keycloak**: Does not support generic OAuth/OIDC providers
- ❌ Significant refactoring required
- ❌ Wrong tool for this use case

**Effort**: High (significant code changes, architectural mismatch)

**Verdict**: Not suitable for this Keycloak-focused library

---

### Option 3: Custom Solution with IdentityModel ❌ **Not Recommended**

**Description**: Build custom token management using lower-level IdentityModel library

**Pros**:
- Maximum flexibility
- No vendor lock-in
- Can optimize for specific use cases

**Cons**:
- ❌ Significant development effort required
- ❌ Need to implement token caching, refresh logic, expiration handling
- ❌ Higher maintenance burden
- ❌ Reinventing the wheel
- ❌ Higher risk of bugs
- ❌ No immediate benefit over existing solution

**Effort**: Very High (custom implementation + ongoing maintenance)

**Verdict**: Not justified given Duende's active maintenance

---

## Migration Impact Analysis

### If We Stay with Duende (Recommended)

**Code Changes**: None required  
**Documentation Changes**: Minor updates to reference new repository  
**Testing Required**: None (already in use)  
**Risk Level**: Minimal  
**Timeline**: Immediate

### If We Migrate to Alternative

**Code Changes**: Extensive refactoring across multiple files  
**Documentation Changes**: Complete rewrite of token management docs  
**Testing Required**: Full integration test suite  
**Risk Level**: High  
**Timeline**: Several weeks

---

## Technical Details

### Token Management Requirements for This Project

1. **Client Credentials Flow**: Service-to-service authentication
2. **Token Caching**: Reduce calls to token endpoint
3. **Automatic Refresh**: Handle token expiration transparently
4. **Generic OAuth/OIDC**: Support Keycloak and other providers
5. **Integration**: Works with HttpClient pipeline

### How Duende Meets Requirements

```csharp
// Current implementation (no changes needed)
services.AddDistributedMemoryCache();
services
    .AddClientCredentialsTokenManagement()
    .AddClient(
        ClientCredentialsClientName.Parse(adminClient),
        client =>
        {
            client.ClientId = ClientId.Parse(options.Resource);
            client.ClientSecret = ClientSecret.Parse(options.Credentials.Secret);
            client.TokenEndpoint = new Uri(options.KeycloakTokenEndpoint);
        }
    );

services
    .AddKiotaKeycloakAdminHttpClient(builder.Configuration)
    .AddClientCredentialsTokenHandler(ClientCredentialsClientName.Parse(adminClient));
```

**Features provided**:
- ✅ Automatic token acquisition
- ✅ Token caching with configurable expiration
- ✅ Automatic token refresh
- ✅ HttpClient integration via DelegatingHandler
- ✅ Support for multiple named clients
- ✅ Distributed cache support

---

## Recommendations

### Immediate Actions

1. ✅ **Continue using Duende.AccessTokenManagement v4.1.0**
2. 📝 **Update documentation** to reference new FOSS repository:
   - Update `docs/admin-rest-api/access-token.md`
   - Update `src/Keycloak.AuthServices.Sdk/README.md`
   - Clarify that package is actively maintained

### Optional Actions

- Add comment in code referencing new repository location
- Monitor DuendeSoftware/foss repository for updates
- Review changelog when upgrading to newer versions

### Not Recommended

- ❌ Migrating to Microsoft.Identity.Web (incompatible with Keycloak)
- ❌ Building custom token management solution (unnecessary effort)
- ❌ Removing Duende dependency (no viable alternative)

---

## Conclusion

The Duende.AccessTokenManagement package is **not obsolete or abandoned**. The archived repository status is due to a reorganization into a monorepo structure, which is a best practice in modern software development. The package continues to receive active maintenance, supports the latest .NET versions, and remains the best solution for OAuth/OIDC token management in .NET applications working with generic identity providers like Keycloak.

**Final Recommendation**: ✅ No migration required. Continue using Duende.AccessTokenManagement with minor documentation updates.

---

## References

- **Active Repository**: https://github.com/DuendeSoftware/foss/tree/main/access-token-management
- **Archived Repository**: https://github.com/DuendeArchive/Duende.AccessTokenManagement
- **NuGet Package**: https://www.nuget.org/packages/Duende.AccessTokenManagement/
- **Documentation**: https://docs.duendesoftware.com/accesstokenmanagement/
- **License**: Apache 2.0 - https://github.com/DuendeSoftware/foss/blob/main/LICENSE

---

**Investigation completed**: January 23, 2026  
**Related Issue**: [#188](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/issues/188)
