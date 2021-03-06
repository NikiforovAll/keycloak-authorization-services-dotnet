using Api;
using Api.Filters;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

host.ConfigureLogger();
host.ConfigureKeycloakConfigurationSource("keycloak.json");

services.AddInfrastructure(configuration);

#pragma warning disable ASP0000
DatabaseUtils.MigrateDatabase(services.BuildServiceProvider());
#pragma warning restore ASP0000

services
    .AddApplication()
    .AddSwagger();

// adds client resource claims transformation
services.AddKeycloakAuthentication(configuration, o =>
{
    o.RequireHttpsMetadata = false;
});

services.AddAuthorization(o =>
{
    o.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    o.AddPolicy(PolicyConstants.MyCustomPolicy, b =>
    {
        // b.AddRequirements(new DecisionRequirement("workspaces", "workspaces:read"));
        b.RequireProtectedResource("workspaces", "workspaces:read");
    });

    o.AddPolicy(PolicyConstants.CanDeleteAllWorkspaces, b =>
    {
        b.RequireRealmRoles("SuperManager");
    });

    o.AddPolicy(PolicyConstants.AccessManagement, b =>
    {
        b.RequireResourceRoles("Manager");
    });
}).AddKeycloakAuthorization(configuration);

services.AddSingleton<IAuthorizationPolicyProvider, ProtectedResourcePolicyProvider>();

services.AddControllers(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());

services.AddKeycloakAdminHttpClient(configuration);

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();
