namespace TestWebApiWithControllers;

internal class Program
{
#pragma warning disable IDE0210 // Convert to top-level statements
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
#pragma warning restore IDE0210 // Convert to top-level statements
}
