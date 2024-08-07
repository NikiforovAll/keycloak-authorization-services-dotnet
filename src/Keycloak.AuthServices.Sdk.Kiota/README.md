# Keycloak.AuthServices.Sdk.Kiota

## Generate Client

```bash
export KEYCLOAK_API_VERSION=25.0.2
kiota generate -l CSharp \
    --log-level trace \
    --output ./ \
    --namespace-name Keycloak.AuthServices.Sdk.Kiota.Admin \
    --class-name KeycloakAdminApiClient \
    --exclude-backward-compatible \
    --openapi https://www.keycloak.org/docs-api/$KEYCLOAK_API_VERSION/rest-api/openapi.json
```
q
