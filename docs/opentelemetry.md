# Keycloak.AuthServices.OpenTelemetry

`Keycloak.AuthServices` can be instrumented via [OpenTelemetry](https://opentelemetry.io/docs/languages/net/getting-started/).

You may ask, **"Why do I even need it?"** and you would be right. In most cases, logging is enough. However, since `Keycloak.AuthServices.Authorization` makes multiple outgoing requests to the Authorization Server, it was decided to add OpenTelemetry support to gain better insights into how the authorization process works.

## Add to your code

```bash
dotnet add package Keycloak.AuthServices.OpenTelemetry
```

Here is how to use it:

```csharp
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
});

services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddKeycloakAuthServicesInstrumentation() // [!code highlight]
    )
    .WithTracing(tracing =>
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddKeycloakAuthServicesInstrumentation() // [!code highlight]
    )
    .UseOtlpExporter();
```

## Metrics example

```bash
dotnet counters monitor \
    --name ResourceAuthorization \
    --counters Keycloak.AuthServices.Authorization
```

Play around with an application and see the results:

```text
Press p to pause, r to resume, q to quit.
    Status: Running

Name                                                               Current Value
[Keycloak.AuthServices.Authorization]
    keycloak.authservices.requirements.fail (Count)
        requirement=ParameterizedProtectedResourceRequirement              3
    keycloak.authservices.requirements.succeed (Count)
        requirement=ParameterizedProtectedResourceRequirement              5
        requirement=RealmAccessRequirement                                16

```
