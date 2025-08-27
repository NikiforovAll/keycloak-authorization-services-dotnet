# Keycloak.AuthServices.Sdk.Kiota

## Generate Client

Before generating the client, delete 
- Admin and Models directory
- KeycloakAdminApiClient.cs

```bash
export KEYCLOAK_API_VERSION=26.3.3
kiota generate -l CSharp \
    --log-level trace \
    --output ./ \
    --namespace-name Keycloak.AuthServices.Sdk.Kiota.Admin \
    --class-name KeycloakAdminApiClient \
    --exclude-backward-compatible \
    --openapi https://www.keycloak.org/docs-api/$KEYCLOAK_API_VERSION/rest-api/openapi.json
```

Sample script with powershell and docker

```powershell
$KIOTA_VERSION="1.28.0"
$KEYCLOAK_API_VERSION="26.3.3"
docker run -v ${PWD}:/app/output `
    mcr.microsoft.com/openapi/kiota:$KIOTA_VERSION `
    generate --language CSharp `
    --namespace-name Keycloak.AuthServices.Sdk.Kiota.Admin `
    --class-name KeycloakAdminApiClient `
    --exclude-backward-compatible `
    --openapi https://www.keycloak.org/docs-api/$KEYCLOAK_API_VERSION/rest-api/openapi.json
```

Update `Keycloak.Authservices.Sdk.Kiota.csproj` and set `VersionPrefix` to the Keycloak api version