namespace Aspire.Hosting;

using Aspire.Hosting.ApplicationModel;

/// <summary>
/// Provides extension methods for adding Keycloak resources to an <see cref="IDistributedApplicationBuilder"/>.
/// </summary>
public static class KeycloakBuilderExtensions
{
    private const string UserEnvVarName = "KEYCLOAK_ADMIN";
    private const string PasswordEnvVarName = "KEYCLOAK_ADMIN_PASSWORD";
    private const int DefaultContainerPort = 8080;

    /// <summary>
    /// Adds a Keycloak resource to the application model. A container is used for local development.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/>.</param>
    /// <param name="name">The name of the resource. </param>
    /// <param name="tag"></param>
    /// <param name="userName">The parameter used to provide the user name for the Keycloak resource.</param>
    /// <param name="password">The parameter used to provide the administrator password for the Keycloak resource. </param>
    /// <param name="port">The host port used when launching the container.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<KeycloakResource> AddKeycloakContainer(
        this IDistributedApplicationBuilder builder,
        string name,
        string? tag = null,
        IResourceBuilder<ParameterResource>? userName = null,
        IResourceBuilder<ParameterResource>? password = null,
        int? port = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        var server = new KeycloakResource(name, userName?.Resource, password?.Resource);

        return builder
            .AddResource(server)
            .WithHttpEndpoint(
                port: port ?? DefaultContainerPort,
                targetPort: DefaultContainerPort,
                name: KeycloakResource.PrimaryEndpointName
            )
            .WithImage(KeycloakContainerImageTags.Image, tag ?? KeycloakContainerImageTags.Tag)
            .WithImageRegistry(KeycloakContainerImageTags.Registry)
            .WithEnvironment(context =>
            {
                context.EnvironmentVariables[UserEnvVarName] = server.UserNameReference;
                context.EnvironmentVariables[PasswordEnvVarName] = server.PasswordReference;
            })
            .WithAnnotation(
                new CommandLineArgsCallbackAnnotation(args =>
                {
                    args.Clear();

                    if (builder.ExecutionContext.IsRunMode)
                    {
                        args.Add("start-dev");
                    }
                    else
                    {
                        args.Add("start");
                    }
                    args.Add("--import-realm");
                })
            );
    }

    /// <summary>
    /// Adds a Realm to the application model.
    /// </summary>
    /// <param name="builder">The Keycloak server resource builder.</param>
    /// <param name="name">The name of the resource. </param>
    /// <param name="realm">The name of the Realm.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<KeycloakRealmResource> AddRealm(
        this IResourceBuilder<KeycloakResource> builder,
        string name,
        string? realm = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        var realmResource = new KeycloakRealmResource(name, realm ?? name, builder.Resource);

        return builder.ApplicationBuilder.AddResource(realmResource).ExcludeFromManifest();
    }

    /// <summary>
    /// Adds a Realm to the application model.
    /// </summary>
    /// <param name="builder">The Keycloak server resource builder.</param>
    /// <param name="import"></param>
    /// <param name="isReadOnly"></param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<KeycloakResource> WithImport(
        this IResourceBuilder<KeycloakResource> builder,
        string import,
        bool isReadOnly = false
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Resource.WithImport(import);

        if (Directory.Exists(import))
        {
            builder.WithBindMount(import, "/opt/keycloak/data/import", isReadOnly);
        }
        else
        {
            var fileName = Path.GetFileName(import);

            builder.WithBindMount(import, $"/opt/keycloak/data/import/{fileName}", isReadOnly);
        }

        return builder;
    }

    /// <summary>
    /// Adds a named volume for the data folder to a PostgreSQL container resource.
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="name">The name of the volume. Defaults to an auto-generated name based on the application and resource names.</param>
    /// <param name="isReadOnly">A flag that indicates if this is a read-only volume.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<KeycloakResource> WithDataVolume(
        this IResourceBuilder<KeycloakResource> builder,
        string? name = null,
        bool isReadOnly = false
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.WithVolume(
            name ?? VolumeNameGenerator.CreateVolumeName(builder, "data"),
            "/opt/keycloak/data",
            isReadOnly
        );
    }

    /// <summary>
    /// Adds a reference to a Keycloak resource to the specified resource builder.
    /// </summary>
    /// <typeparam name="TResource">The type of the resource builder.</typeparam>
    /// <param name="builder">The resource builder to add the reference to.</param>
    /// <param name="keycloakBuilder">The Keycloak resource builder to reference.</param>
    /// <param name="configurationPrefix"></param>
    /// <returns>The resource builder with the added reference.</returns>
    public static IResourceBuilder<TResource> WithReference<TResource>(
        this IResourceBuilder<TResource> builder,
        IResourceBuilder<KeycloakRealmResource> keycloakBuilder,
        string configurationPrefix = "Keycloak"
    )
        where TResource : IResourceWithEnvironment
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(keycloakBuilder);

        builder.WithEnvironment(
            $"{configurationPrefix}__AuthServerUrl",
            keycloakBuilder.Resource.Parent.PrimaryEndpoint
        );

        builder.WithEnvironment($"{configurationPrefix}__Realm", keycloakBuilder.Resource.Realm);

        return builder;
    }
}

internal static class KeycloakContainerImageTags
{
    public const string Registry = "quay.io";
    public const string Image = "keycloak/keycloak";
    public const string Tag = "24.0.3";
}

internal static class VolumeNameGenerator
{
    /// <summary>
    /// Creates a volume name with the form <c>$"{applicationName}-{resourceName}-{suffix}</c>, e.g. <c>"myapplication-postgres-data"</c>.
    /// </summary>
    /// <remarks>
    /// If the application name contains chars that are invalid for a volume name, the prefix <c>"volume-"</c> will be used instead.
    /// </remarks>
    public static string CreateVolumeName<T>(IResourceBuilder<T> builder, string suffix)
        where T : IResource
    {
        if (!HasOnlyValidChars(suffix))
        {
            throw new ArgumentException(
                $"The suffix '{suffix}' contains invalid characters. Only [a-zA-Z0-9_.-] are allowed.",
                nameof(suffix)
            );
        }

        // Create volume name like "myapplication-postgres-data"
        var applicationName = builder.ApplicationBuilder.Environment.ApplicationName;
        var resourceName = builder.Resource.Name;
        return $"{(HasOnlyValidChars(applicationName) ? applicationName : "volume")}-{resourceName}-{suffix}";
    }

    private static bool HasOnlyValidChars(string name)
    {
        // According to the error message from docker CLI, volume names must be of form "[a-zA-Z0-9][a-zA-Z0-9_.-]"
        var nameSpan = name.AsSpan();

        for (var i = 0; i < nameSpan.Length; i++)
        {
            var c = nameSpan[i];

            if (i == 0 && !(char.IsAsciiLetter(c) || char.IsNumber(c)))
            {
                // First char must be a letter or number
                return false;
            }
            else if (
                !(char.IsAsciiLetter(c) || char.IsNumber(c) || c == '_' || c == '.' || c == '-')
            )
            {
                // Subsequent chars must be a letter, number, underscore, period, or hyphen
                return false;
            }
        }

        return true;
    }
}
