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
/// The page is the Radioactive template (Assets/WebGLTemplates/Radioactive), with a
/// loading screen. The build is gzip-compressed and the browser decompresses it itself
/// (decompression fallback), so a plain static file server needs no Content-Encoding
/// headers. Data caching keeps it in the browser for the next visit.
/// </summary>
public static class WebGLBuild
{
    private const string OutputPathArgument = "-outputPath";
    // Served by Locus as the static application radioactive-web (--directory web)
    private const string DefaultOutputPath = "web";
    private const string Template = "PROJECT:Radioactive";
    // Unity's output; the rest of the output directory (Dockerfile, .dockerignore) is not its own.
    private const string BuildDirectory = "Build";

    public static void Build()
    {
        PlayerSettings.WebGL.template = Template;
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
        PlayerSettings.WebGL.decompressionFallback = true;
        PlayerSettings.WebGL.dataCaching = true;

        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        string outputPath = OutputPath();
        RemovePreviousBuild(outputPath);

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
    }

    // File names change with the settings (.unityweb when compressed); old ones would stay behind.
    private static void RemovePreviousBuild(string outputPath)
    {
        string buildPath = Path.Combine(outputPath, BuildDirectory);

        if (Directory.Exists(buildPath))
        {
            Directory.Delete(buildPath, true);
        }
    }

    private static string OutputPath()
    {
        string[] arguments = Environment.GetCommandLineArgs();
        int index = Array.IndexOf(arguments, OutputPathArgument);

        return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : DefaultOutputPath;
    }
}
