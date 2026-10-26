using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

/// <summary>
/// Builds the WebGL player from the command line:
///
///   Unity -batchmode -quit -projectPath . -buildTarget WebGL \
///         -executeMethod WebGLBuild.Build [-outputPath web]
///
/// Uncompressed output, so any static file server can serve it without
/// Content-Encoding headers for .br or .gz files.
/// </summary>
public static class WebGLBuild
{
    private const string OutputPathArgument = "-outputPath";
    // Served by Locus as the static application radioactive-web (--directory web)
    private const string DefaultOutputPath = "web";
    private const string IndexFile = "index.html";
    private const string TitleTag = "<title>";
    private const string TemplateTitlePrefix = "<title>Unity Web Player | ";

    public static void Build()
    {
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
        PlayerSettings.WebGL.decompressionFallback = false;

        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        string outputPath = OutputPath();

        BuildReport report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = BuildTarget.WebGL,
            options = BuildOptions.None,
        });

        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new Exception("WebGL build failed: " + report.summary.result);
        }

        UseProductNameAsTitle(outputPath);
    }

    // The default template titles the page "Unity Web Player | <product>"; the game is just its name.
    private static void UseProductNameAsTitle(string outputPath)
    {
        string indexPath = Path.Combine(outputPath, IndexFile);
        string page = File.ReadAllText(indexPath);
        File.WriteAllText(indexPath, page.Replace(TemplateTitlePrefix, TitleTag));
    }

    private static string OutputPath()
    {
        string[] arguments = Environment.GetCommandLineArgs();
        int index = Array.IndexOf(arguments, OutputPathArgument);

        return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : DefaultOutputPath;
    }
}
