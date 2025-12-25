# Keycloak + WebApi

## Configure Keycloak

Run docker image locally, as described in the [documentation](https://www.keycloak.org/getting-started/getting-started-docker).

```bash
docker run -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:26.4.2 start-dev
```

For Keycloak configuration instructions see: [Keycloak Configuration](/configuration/configuration-keycloak)
