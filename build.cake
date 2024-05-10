var target = Argument("Target", "Default");
var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") is not null ? EnvironmentVariable("Configuration") :
    "Release";

var artefactsDirectory = Directory("./Artefacts");

Task("Clean")
    .Description("Cleans the artefacts, bin and obj directories.")
    .Does(() =>
    {
        CleanDirectory(artefactsDirectory);
        DeleteDirectories(GetDirectories("**/bin"), new DeleteDirectorySettings() { Force = true, Recursive = true });
        DeleteDirectories(GetDirectories("**/obj"), new DeleteDirectorySettings() { Force = true, Recursive = true });
    });

Task("Restore")
    .Description("Restores NuGet packages.")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetRestore();
    });

Task("Build")
    .Description("Builds the solution.")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetBuild(".", new DotNetBuildSettings
        {
            Configuration = configuration,
            OutputDirectory = artefactsDirectory,
        });
    });

Task("Test")
    .Description("Runs unit tests and outputs test results to the artefacts directory.")
    .DoesForEach(GetFiles("./tests/**/*.csproj").Where(file => !file.ToString().Contains("Integration")), project =>
    {
        DotNetTest(
            project.ToString(),
            new DotNetTestSettings()
            {
                Collectors = new string[] { "XPlat Code Coverage" },
                Configuration = configuration,
                Loggers = new string[]
                {
                    $"trx;LogFileName={project.GetFilenameWithoutExtension()}.trx",
                    $"html;LogFileName={project.GetFilenameWithoutExtension()}.html",
                },
                NoBuild = false,
                NoRestore = false,
                ResultsDirectory = artefactsDirectory,
                ArgumentCustomization = x => x.Append("--blame"),
            });
    });

Task("Pack")
    .Description("Creates NuGet packages and outputs them to the artefacts directory.")
    .Does(() =>
    {
        var buildSettings = new DotNetMSBuildSettings()
        {
        };
        if (!BuildSystem.IsLocalBuild)
        {
            buildSettings.WithProperty("ContinuousIntegrationBuild", "true");
        }

        DotNetPack(
            ".",
            new DotNetPackSettings()
            {
                Configuration = configuration,
                IncludeSymbols = true,
                MSBuildSettings = buildSettings,
                NoBuild = false,
                NoRestore = false,
                OutputDirectory = artefactsDirectory,
            });
    }).IsDependentOn("Build");

Task("Default")
    .Description("Cleans, restores NuGet packages, builds the solution, runs unit tests and then creates NuGet packages.")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

RunTarget(target);
