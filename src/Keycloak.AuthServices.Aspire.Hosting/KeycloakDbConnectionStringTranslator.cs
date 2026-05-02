namespace Aspire.Hosting;

using Aspire.Hosting.ApplicationModel;

internal enum KeycloakDbVendor
{
    Postgres,
    MySql,
    MariaDb,
    MsSql,
    Oracle,
}

internal static class KeycloakDbReferenceBuilder
{
    public static KeycloakDbSettings Build(
        KeycloakDbVendor vendor,
        IResourceWithConnectionString resource
    )
    {
        ArgumentNullException.ThrowIfNull(resource);

        var props = new Dictionary<string, ReferenceExpression>(StringComparer.OrdinalIgnoreCase);
        foreach (var kvp in resource.GetConnectionProperties())
        {
            props[kvp.Key] = kvp.Value;
        }

        var kcDb = vendor switch
        {
            KeycloakDbVendor.Postgres => "postgres",
            KeycloakDbVendor.MySql => "mysql",
            KeycloakDbVendor.MariaDb => "mariadb",
            KeycloakDbVendor.MsSql => "mssql",
            KeycloakDbVendor.Oracle => "oracle",
            _ => throw new ArgumentOutOfRangeException(nameof(vendor)),
        };

        // Prefer resource-provided JDBC expression when available; it preserves
        // container-network host/port resolution via the underlying endpoint references.
        var url = props.TryGetValue("JdbcConnectionString", out var jdbc)
            ? jdbc
            : BuildUrlFromParts(vendor, props, resource.Name);

        props.TryGetValue("Username", out var user);
        props.TryGetValue("Password", out var pwd);

        return new(kcDb, url, user, pwd);
    }

    private static ReferenceExpression BuildUrlFromParts(
        KeycloakDbVendor vendor,
        IReadOnlyDictionary<string, ReferenceExpression> props,
        string resourceName
    )
    {
        if (!props.TryGetValue("Host", out var host) || !props.TryGetValue("Port", out var port))
        {
            throw new InvalidOperationException(
                $"Database resource '{resourceName}' did not expose 'Host'/'Port' connection properties required to build a Keycloak JDBC URL."
            );
        }

        props.TryGetValue("DatabaseName", out var db);

        return vendor switch
        {
            KeycloakDbVendor.Postgres => db is null
                ? ReferenceExpression.Create($"jdbc:postgresql://{host}:{port}")
                : ReferenceExpression.Create($"jdbc:postgresql://{host}:{port}/{db}"),
            KeycloakDbVendor.MySql => db is null
                ? ReferenceExpression.Create($"jdbc:mysql://{host}:{port}")
                : ReferenceExpression.Create($"jdbc:mysql://{host}:{port}/{db}"),
            KeycloakDbVendor.MariaDb => db is null
                ? ReferenceExpression.Create($"jdbc:mariadb://{host}:{port}")
                : ReferenceExpression.Create($"jdbc:mariadb://{host}:{port}/{db}"),
            KeycloakDbVendor.MsSql => db is null
                ? ReferenceExpression.Create($"jdbc:sqlserver://{host}:{port}")
                : ReferenceExpression.Create($"jdbc:sqlserver://{host}:{port};databaseName={db}"),
            KeycloakDbVendor.Oracle => db is null
                ? ReferenceExpression.Create($"jdbc:oracle:thin:@//{host}:{port}")
                : ReferenceExpression.Create($"jdbc:oracle:thin:@//{host}:{port}/{db}"),
            _ => throw new ArgumentOutOfRangeException(nameof(vendor)),
        };
    }
}

internal readonly record struct KeycloakDbSettings(
    string KcDb,
    ReferenceExpression JdbcUrl,
    ReferenceExpression? Username,
    ReferenceExpression? Password
);
