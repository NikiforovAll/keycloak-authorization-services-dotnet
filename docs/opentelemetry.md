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

services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddKeycloakAuthServicesInstrumentation(); // [!code highlight]
    })
    .UseOtlpExporter();
```
