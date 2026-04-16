namespace Aspire.Hosting;

/// <summary>
/// Provides database connection parameters for a Keycloak container in JDBC format.
/// </summary>
/// <remarks>
/// Implement this interface for your specific database technology and pass it to
/// <see cref="KeycloakBuilderExtensions.WithDatabase"/>.
/// The adapter translates between your Aspire database resource and the
/// Keycloak-specific environment variables (<c>KC_DB</c>, <c>KC_DB_URL</c>,
/// <c>KC_DB_USERNAME</c>, <c>KC_DB_PASSWORD</c>).
/// </remarks>
/// <example>
/// A PostgreSQL adapter using an Aspire Postgres database resource:
/// <code>
/// public sealed class PostgresKeycloakDbAdapter(IResourceBuilder&lt;PostgresDatabaseResource&gt; db)
///     : IKeycloakDbAdapter
/// {
///     public string DbVendor => "postgres";
///
///     public Task&lt;string&gt; GetDbUrlAsync(CancellationToken ct = default)
///     {
///         var host = db.Resource.Parent.PrimaryEndpoint.Host;
///         var port = db.Resource.Parent.PrimaryEndpoint.Port;
///         return Task.FromResult($"jdbc:postgresql://{host}:{port}/{db.Resource.DatabaseName}");
///     }
///
///     public Task&lt;string?&gt; GetDbUsernameAsync(CancellationToken ct = default) =>
///         Task.FromResult&lt;string?&gt;(db.Resource.Parent.UserNameParameter?.Value ?? "postgres");
///
///     public Task&lt;string?&gt; GetDbPasswordAsync(CancellationToken ct = default) =>
///         Task.FromResult&lt;string?&gt;(db.Resource.Parent.PasswordParameter?.Value);
/// }
/// </code>
/// Usage:
/// <code>
/// var pg = builder.AddPostgres("postgres").AddDatabase("keycloak");
/// var keycloak = builder
///     .AddKeycloakContainer("keycloak")
///     .WaitFor(pg)
///     .WithDatabase(new PostgresKeycloakDbAdapter(pg));
/// </code>
/// </example>
public interface IKeycloakDbAdapter
{
    /// <summary>
    /// Gets the Keycloak database vendor identifier.
    /// </summary>
    /// <remarks>
    /// Corresponds to the <c>KC_DB</c> environment variable.
    /// Supported values: <c>postgres</c>, <c>mysql</c>, <c>mariadb</c>, <c>mssql</c>, <c>oracle</c>.
    /// </remarks>
    string DbVendor { get; }

    /// <summary>
    /// Gets the JDBC connection URL for the database.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The JDBC URL (e.g., <c>jdbc:postgresql://localhost:5432/keycloak</c>).</returns>
    /// <remarks>
    /// Corresponds to the <c>KC_DB_URL</c> environment variable.
    /// </remarks>
    Task<string> GetDbUrlAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the database username, or <c>null</c> if not applicable.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The database username, or <c>null</c> to omit <c>KC_DB_USERNAME</c>.</returns>
    /// <remarks>
    /// Corresponds to the <c>KC_DB_USERNAME</c> environment variable.
    /// </remarks>
    Task<string?> GetDbUsernameAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the database password, or <c>null</c> if not applicable.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The database password, or <c>null</c> to omit <c>KC_DB_PASSWORD</c>.</returns>
    /// <remarks>
    /// Corresponds to the <c>KC_DB_PASSWORD</c> environment variable.
    /// </remarks>
    Task<string?> GetDbPasswordAsync(CancellationToken cancellationToken = default);
}
