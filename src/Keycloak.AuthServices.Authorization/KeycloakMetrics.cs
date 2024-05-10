namespace Keycloak.AuthServices.Authorization;

using System.Diagnostics.Metrics;

/// <summary>
/// Represents a class for tracking Keycloak authorization metrics.
/// </summary>
public class KeycloakMetrics
{
    private readonly Counter<long> requirementsFailed;
    private readonly Counter<long> requirementsSucceeded;
    private readonly Counter<long> requirementsErrored;
    private readonly Counter<long> requirementsSkipped;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakMetrics"/> class.
    /// </summary>
    /// <param name="meterFactory">The meter factory used to create metrics.</param>
    public KeycloakMetrics(IMeterFactory meterFactory)
    {
#pragma warning disable CA2000 // Dispose objects before losing scope
        var meter = meterFactory.Create("Keycloak.AuthServices.Authorization");
#pragma warning restore CA2000 // Dispose objects before losing scope
        this.requirementsFailed = meter.CreateCounter<long>(
            $"{ActivityConstants.Namespace}.requirements.fail"
        );
        this.requirementsSucceeded = meter.CreateCounter<long>(
            $"{ActivityConstants.Namespace}.requirements.succeed"
        );
        this.requirementsErrored = meter.CreateCounter<long>(
            $"{ActivityConstants.Namespace}.requirements.error"
        );
        this.requirementsSkipped = meter.CreateCounter<long>(
            $"{ActivityConstants.Namespace}.requirements.skipped"
        );
    }

    /// <summary>
    /// Records a failed requirement.
    /// </summary>
    /// <param name="requirement">The name of the failed requirement.</param>
    public void FailRequirement(string requirement) =>
        this.requirementsFailed.Add(
            1,
            new KeyValuePair<string, object?>("requirement", requirement)
        );

    /// <summary>
    /// Records a succeeded requirement.
    /// </summary>
    /// <param name="requirement">The name of the succeeded requirement.</param>
    public void SucceedRequirement(string requirement) =>
        this.requirementsSucceeded.Add(
            1,
            new KeyValuePair<string, object?>("requirement", requirement)
        );

    /// <summary>
    /// Records an errored requirement.
    /// </summary>
    /// <param name="requirement">The name of the errored requirement.</param>
    public void ErrorRequirement(string requirement) =>
        this.requirementsErrored.Add(
            1,
            new KeyValuePair<string, object?>("requirement", requirement)
        );


    /// <summary>
    /// Records an skipped requirement.
    /// </summary>
    /// <param name="requirement">The name of the skipped requirement.</param>
    public void SkipRequirement(string requirement) =>
        this.requirementsSkipped.Add(
            1,
            new KeyValuePair<string, object?>("requirement", requirement)
        );
}
