# Troubleshooting

*Common issues:*

[[toc]]

## I receive 401 Unauthorized status code

* [Turn on](/qa/recipes.html#how-to-debug-an-application) Debug or Trace logging level and see the logs output
* Make sure access token is provided in Authorization Header.
* Make sure the audience is mapped to a token via [audience mapper](/configuration/configuration-keycloak#add-audience-mapper). You can try to disable audience validation temporarily.
* Make sure the HTTPs requirement is turned off in Development Mode. `KeycloakAuthenticationOptions.SslRequired="none"`

## I receive 403 Forbidden

* [Turn on](/qa/recipes.html#how-to-debug-an-application) Debug or Trace logging level and see the logs output
* In case of RBAC Authorization make sure the `ClaimsPrincipal` has "realm_access" and "resource_access" claims mapped from token issued by Keycloak.
* If you use Keycloak as Authorization Server, make sure it is properly configured and that the Keycloak installation is accessible.

## Keycloak is slow to respond

Keycloak is a central part of the system used by many components. Especially, in Authorization Server scenario where authorization requests are sent to centralized place. Essentially, Keycloak becomes a bottleneck of the system. Consider [Cluster Setup](https://www.keycloak.org/2019/05/keycloak-cluster-setup) to tackle this problem.

Also, you can handle transient HTTP errors by adding resiliency, see [How to setup resiliency to HTTP Clients](/qa/recipes.html#how-to-setup-resiliency-to-http-clients)

> [!NOTE]
> ☝️`Keycloak.AuthServices` supports OpenTelemetry. See [Keycloak.AuthServices.OpenTelemetry](/opentelemetry).
