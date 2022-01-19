Console.Write("hello world");
// using Keycloak.AuthServices.Authentication;
// using Keycloak.AuthServices.Authorization;

// var builder = WebApplication.CreateBuilder(args);
//
// var host = builder.Host;
// var configuration = builder.Configuration;
// var services = builder.Services;
//
// host.ConfigureKeycloakConfigurationSource();
// // conventional registration from keycloak.json
// services.AddKeycloakAuthentication(configuration);
//
// services.AddAuthorization(options =>
//     {
//         options.AddPolicy("RequireWorkspaces", builder =>
//         {
//             builder.RequireProtectedResource("workspaces", "workspaces:read");
//         });
//     })
//     .AddKeycloakAuthorization(configuration);
//
// var app = builder.Build();
//
// app.UseAuthentication()
//     .UseAuthorization();
//
// app.MapGet("/workspaces", () => "[]")
//     .RequireAuthorization("RequireWorkspaces");
//
// app.Run();
