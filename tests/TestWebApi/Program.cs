var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

var endpoints = app
    .MapGroup("/endpoints")
    .RequireAuthorization();

endpoints.MapGet("1", Run);
endpoints.MapGet("2", Run).RequireAuthorization("Policy1");
endpoints.MapGet("3", Run).RequireAuthorization("Policy2");
endpoints.MapGet("4", Run).RequireAuthorization("Policy2");

app.Run();

static Response Run() => new(true);

public partial class Program { }

public record Response(bool Success);
