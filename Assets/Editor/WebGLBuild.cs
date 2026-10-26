using System;
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

    public static void Build()
    {
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
        PlayerSettings.WebGL.decompressionFallback = false;

        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        BuildReport report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = OutputPath(),
            target = BuildTarget.WebGL,
            options = BuildOptions.None,
        });

        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new Exception("WebGL build failed: " + report.summary.result);
        }
    }

    private static string OutputPath()
    {
        string[] arguments = Environment.GetCommandLineArgs();
        int index = Array.IndexOf(arguments, OutputPathArgument);

        return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : DefaultOutputPath;
    }
}
