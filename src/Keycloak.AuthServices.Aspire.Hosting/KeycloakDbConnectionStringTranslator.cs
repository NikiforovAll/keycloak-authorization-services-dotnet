namespace Aspire.Hosting;

using System.Data.Common;

internal enum KeycloakDbVendor
{
    Postgres,
    MySql,
    MariaDb,
    MsSql,
    Oracle,
}

internal static class KeycloakDbConnectionStringTranslator
{
    public static KeycloakDbSettings Translate(KeycloakDbVendor vendor, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        var parsed = new DbConnectionStringBuilder { ConnectionString = connectionString };

        return vendor switch
        {
            KeycloakDbVendor.Postgres => TranslatePostgres(parsed),
            KeycloakDbVendor.MySql => TranslateMySqlLike(parsed, vendor: "mysql", scheme: "mysql"),
            KeycloakDbVendor.MariaDb => TranslateMySqlLike(
                parsed,
                vendor: "mariadb",
                scheme: "mariadb"
            ),
            KeycloakDbVendor.MsSql => TranslateMsSql(parsed),
            KeycloakDbVendor.Oracle => TranslateOracle(parsed),
            _ => throw new ArgumentOutOfRangeException(nameof(vendor)),
        };
    }

    private static KeycloakDbSettings TranslatePostgres(DbConnectionStringBuilder p)
    {
        var host = Get(p, "Host", "Server") ?? "localhost";
        var port = Get(p, "Port") ?? "5432";
        var database = Get(p, "Database");
        var user = Get(p, "Username", "User Id", "User ID", "Uid");
        var password = Get(p, "Password", "Pwd");

        var url = database is null
            ? $"jdbc:postgresql://{host}:{port}"
            : $"jdbc:postgresql://{host}:{port}/{database}";

        return new("postgres", url, user, password);
    }

    private static KeycloakDbSettings TranslateMySqlLike(
        DbConnectionStringBuilder p,
        string vendor,
        string scheme
    )
    {
        var host = Get(p, "Server", "Host", "Data Source") ?? "localhost";
        var port = Get(p, "Port") ?? "3306";
        var database = Get(p, "Database");
        var user = Get(p, "Uid", "User Id", "User ID", "Username");
        var password = Get(p, "Pwd", "Password");

        var url = database is null
            ? $"jdbc:{scheme}://{host}:{port}"
            : $"jdbc:{scheme}://{host}:{port}/{database}";

        return new(vendor, url, user, password);
    }

    private static KeycloakDbSettings TranslateMsSql(DbConnectionStringBuilder p)
    {
        var server = Get(p, "Server", "Data Source") ?? "localhost";
        var (host, port) = SplitMsSqlServer(server);
        var database = Get(p, "Database", "Initial Catalog");
        var user = Get(p, "User Id", "User ID", "Uid", "Username");
        var password = Get(p, "Password", "Pwd");

        var url = $"jdbc:sqlserver://{host}:{port}";
        if (!string.IsNullOrWhiteSpace(database))
        {
            url += $";databaseName={database}";
        }

        return new("mssql", url, user, password);
    }

    private static KeycloakDbSettings TranslateOracle(DbConnectionStringBuilder p)
    {
        var dataSource = Get(p, "Data Source", "Server") ?? "localhost:1521";
        var (host, port, service) = SplitOracleDataSource(dataSource);
        var user = Get(p, "User Id", "User ID", "Username", "Uid");
        var password = Get(p, "Password", "Pwd");

        var url = service is null
            ? $"jdbc:oracle:thin:@//{host}:{port}"
            : $"jdbc:oracle:thin:@//{host}:{port}/{service}";

        return new("oracle", url, user, password);
    }

    private static (string Host, string Port) SplitMsSqlServer(string server)
    {
        var trimmed = server.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase)
            ? server[4..]
            : server;

        var commaIdx = trimmed.IndexOf(',');
        if (commaIdx >= 0)
        {
            return (trimmed[..commaIdx], trimmed[(commaIdx + 1)..]);
        }

        var colonIdx = trimmed.IndexOf(':');
        if (colonIdx >= 0)
        {
            return (trimmed[..colonIdx], trimmed[(colonIdx + 1)..]);
        }

        return (trimmed, "1433");
    }

    private static (string Host, string Port, string? Service) SplitOracleDataSource(string source)
    {
        var slashIdx = source.IndexOf('/');
        var hostPort = slashIdx >= 0 ? source[..slashIdx] : source;
        var service = slashIdx >= 0 ? source[(slashIdx + 1)..] : null;

        var colonIdx = hostPort.IndexOf(':');
        if (colonIdx >= 0)
        {
            return (hostPort[..colonIdx], hostPort[(colonIdx + 1)..], service);
        }

        return (hostPort, "1521", service);
    }

    private static string? Get(DbConnectionStringBuilder p, params string[] keys)
    {
        foreach (var key in keys)
        {
            if (p.TryGetValue(key, out var value) && value is string s && !string.IsNullOrEmpty(s))
            {
                return s;
            }
        }

        return null;
    }
}

internal readonly record struct KeycloakDbSettings(
    string KcDb,
    string JdbcUrl,
    string? Username,
    string? Password
);
