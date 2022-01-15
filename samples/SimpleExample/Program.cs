using Api;
using Api.Data;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
        outputTemplate: "[{Level:u4}] |{SourceContext,30}| {Message:lj}{NewLine}{Exception}",
        restrictedToMinimumLevel: LogEventLevel.Debug)
    .CreateBootstrapLogger();
var services = builder.Services;

builder.Host.UseSerilog();
// Add services to the container.
services.AddInfrastructure(builder.Configuration);

using (var scope = services.BuildServiceProvider().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedDatabase.Run(context);
}
services.AddApplication();
services.AddControllers();
services.AddSwagger();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
