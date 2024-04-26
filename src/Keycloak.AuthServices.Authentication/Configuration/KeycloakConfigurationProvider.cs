namespace Keycloak.AuthServices.Authentication.Configuration;

using System.Globalization;
using System.Text;
using Microsoft.Extensions.Configuration.Json;

/// <summary>
/// Keycloak file configuration provider
/// </summary>
public class KeycloakConfigurationProvider : JsonConfigurationProvider
{
    private readonly string configurationPrefix;

    private const char KeycloakPropertyDelimiter = '-';
    private const char NestedConfigurationDelimiter = ':';
    private const int Utf8LowerCaseDistant = 32;
    private readonly StringBuilder stringBuilder;

    /// <summary>
    /// Initializes a new instance with the specified source.
    /// </summary>
    /// <param name="source">The source settings.</param>
    /// <param name="configurationPrefix"></param>
    public KeycloakConfigurationProvider(
        KeycloakConfigurationSource source,
        string configurationPrefix
    )
        : base(source)
    {
        this.stringBuilder = new StringBuilder();
        this.configurationPrefix = configurationPrefix;
    }

    /// <summary>
    /// Loads the JSON data from a stream.
    /// </summary>
    /// <param name="stream">The stream to read.</param>
    public override void Load(Stream stream)
    {
        base.Load(stream);
        this.Data = this.Data.ToDictionary(
            x => this.NormalizeKey(x.Key),
            x => x.Value,
            StringComparer.OrdinalIgnoreCase
        );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private string NormalizeKey(string key)
    {
        var sections = key.ToUpper(CultureInfo.InvariantCulture)
            .Split(NestedConfigurationDelimiter);

        foreach (var section in sections)
        {
            if (this.stringBuilder.Length != 0)
            {
                this.stringBuilder.Append(NestedConfigurationDelimiter);
            }

            foreach (var x in section.Split(KeycloakPropertyDelimiter))
            {
                for (var i = 0; i < x.Length; i++)
                {
                    var @char = x[i];
                    if (i == 0)
                    {
                        this.stringBuilder.Append(@char);
                    }
                    else
                    {
                        this.stringBuilder.Append((char)(@char + Utf8LowerCaseDistant));
                    }
                }
            }
        }

        var result =
            this.configurationPrefix + NestedConfigurationDelimiter + this.stringBuilder.ToString();

        this.stringBuilder.Clear();

        return result;
    }
}
