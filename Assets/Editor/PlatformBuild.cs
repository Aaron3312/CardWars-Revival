using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Command-line player builds for the non-web platforms.
// Usage: Unity -batchmode -quit -projectPath . -buildTarget StandaloneOSX -executeMethod PlatformBuild.Build -buildPath Build/Mac/CardWars.app [-development]
public static class PlatformBuild
{
	public static void Build()
	{
		string path = GetArg("-buildPath");
		if (string.IsNullOrEmpty(path))
		{
			Debug.LogError("PlatformBuild: -buildPath is required");
			EditorApplication.Exit(2);
			return;
		}
		var options = new BuildPlayerOptions
		{
			scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray(),
			locationPathName = path,
			target = EditorUserBuildSettings.activeBuildTarget,
			options = Environment.GetCommandLineArgs().Contains("-development") ? BuildOptions.Development : BuildOptions.None
		};
		BuildReport report = BuildPipeline.BuildPlayer(options);
		Debug.Log("PlatformBuild result: " + report.summary.result + " (" + options.target + "), errors: " + report.summary.totalErrors + ", size: " + report.summary.totalSize);
		EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
	}

	private static string GetArg(string name)
	{
		string[] args = Environment.GetCommandLineArgs();
		int i = Array.IndexOf(args, name);
		return (i >= 0 && i + 1 < args.Length) ? args[i + 1] : null;
	}
}
