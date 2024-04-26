# Keycloak.AuthServices.Sdk

## Generate

```bash
kiota generate -l CSharp \
    --log-level trace \
    --output ./ \
    --namespace-name Keycloak.AuthServices.Sdk.Admin \
    --class-name KeycloakAdminApiClient \
    --exclude-backward-compatible \
    --openapi ./openapi/openapi.json
```
